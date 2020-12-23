# IdGenerator <a href="https://www.nuget.org/packages/IdGenerator.Net/"><img src="http://img.shields.io/nuget/v/IdGenerator.Net.svg?style=flat-square" alt="NuGet version" height="18"></a>
IdGenerator, include SnowFlake and Id Obfuscator
[中文](https://github.com/choby/IdGenerator/blob/main/README-ZH.md)

repository: [https://github.com/choby/IdGenerator](https://github.com/choby/IdGenerator)

```powershell
Install-Package IdGenerator.Net
```

## SnowFlake

SnowFlake is an algorithm adopted by Twitter, whose purpose is to generate globally unique and trend-increasing IDs in a distributed system.



How many globally unique IDs can the SnowFlake algorithm generate in the same millisecond? 
Number of IDs in the same millisecond = 1024 X 4096 = 4194304

```csharp
var worker = new IdWorker(1, 1);
var id = worker.NextId();
```

## id Obfuscator (Can be used to confuse auto-increment id)
### Analysis of self-increasing id problem:

In the scenario of generating short links, an ID number is used for each long link, which solves the problem of lossy compression and duplication, but increasing the id will cause great defects. At the same time, because of the large amount of data, it is impossible for a new long connection to check whether it already exists in the database by querying the database, because it is difficult to index long fields in the database, which will cause long link duplication.

advantage：generate a limited id based on a limited long chain and will not repeat.

disadvantage：exposure of self-incremented IDs poses a great security risk, resulting in the disclosure of link information; repeated queries on long links are not supported.

### solution

The core of hiding the increasing law is the use of Feistel password structure：

The input of the Feistel encryption algorithm is a plaintext with a length of 2w and a key K=(K1, K2..., Kn). The plaintext group is divided into the left and right halves L and R, and then n iterations are performed. After the iteration is completed, the left and right halves are merged together to generate the ciphertext group.

Feistel encryption algorithm can produce a very useful feature, that is, within a certain number range (2 to the power of n), each number can find a unique matching object based on the key, which achieves our hiding The purpose of increasing law.



```csharp
var idObfuscator = new IdObfuscator();
ulong id = ulong.MaxValue; // 18446744073709551615
var feistelID = idObfuscator.Permute(id); //14585380100699608688
var reFeistelID = idObfuscator.Permute(feistelID); // 18446744073709551615
var base62 = idObfuscator.PermuteToBase62(id); // 62进制:dLNS46oypRo
```
