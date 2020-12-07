# IdGenerator
id生成器, 包含雪花算法和id混淆器

## 雪花算法

```csharp
var worker = new IdWorker(1, 1);
var id = worker.NextId();
```
