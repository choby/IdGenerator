# IdGenerator
id生成器, 包含雪花算法和id混淆器, 项目地址: [https://github.com/choby/IdGenerator](https://github.com/choby/IdGenerator)

```powershell
Install-Package IdGenerator.Net -Version 1.0.0
```

## 雪花算法

SnowFlake是Twitter公司采用的一种算法，目的是在分布式系统中产生全局唯一且趋势递增的ID。

组成部分（64bit）
1.第一位 占用1bit，其值始终是0，没有实际作用。 
2.时间戳 占用41bit，精确到毫秒，总共可以容纳约69年的时间。 
3.工作机器id 占用10bit，其中高位5bit是数据中心ID，低位5bit是工作节点ID，做多可以容纳1024个节点。 
4.序列号 占用12bit，每个节点每毫秒0开始不断累加，最多可以累加到4095，一共可以产生4096个ID。

SnowFlake算法在同一毫秒内最多可以生成多少个全局唯一ID呢：： 同一毫秒的ID数量 = 1024 X 4096 = 4194304

```csharp
var worker = new IdWorker(1, 1);
var id = worker.NextId();
```

## id混淆器 (可用于混淆自增id)
### 自增id问题分析:
生成短连接的场景中,采用给每个长链接一个ID号，解决了有损压缩和重复的问题，但是因为递增id会造成很大的缺陷。同时因为在大数据量的情况下，新来一个长连接我们不可能通过查询数据库的方式来确认是否已经在数据库存在，因为超长字段在数据库很难被索引，这样就会造成长链接重复。

优点：根据有限的长链生成有限的id，不会重复。

缺点：自增id暴露在外，有很大的安全风险，造成链接信息泄露；不支持长链接重复查询。

### 解决方案

隐藏递增规律的核心就是使用Feistel 密码结构：

Feistel加密算法的输入是长为2w的明文和一个密钥K=（K1，K2...,Kn）。将明文分组分成左右两半L和R，然后进行n轮迭代，迭代完成后，再将左右两半合并到一起以产生密文分组。

Feistel加密算法能够产生一个非常好用的特点，那就是，在一定的数字范围内（2的n次方），每一个数字都能根据密钥找到唯一的一个匹配对象，这就达到了我们隐藏递增规律的目的。



```csharp
var idObfuscator = new IdObfuscator();
ulong i = ulong.MaxValue; // 18446744073709551615
var feistelID = idObfuscator.Permute(id); //14585380100699608688
var reFeistelID = idObfuscator.Permute(feistelID); // 18446744073709551615
var base62 = idObfuscator.PermuteToBase62(id); // 62进制:dLNS46oypRo
```
