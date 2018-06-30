``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |     Mean |    Error |    StdDev |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |---------:|---------:|----------:|-----------:|----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **317.8 ms** | **5.324 ms** |  **4.980 ms** | **35250.0000** | **1562.5000** | **1500.0000** | **215.82 MB** |
| FileHelpers |                           False | 124.7 ms | 2.443 ms |  4.527 ms | 22375.0000 | 1562.5000 | 1500.0000 | 157.84 MB |
|    **FlatFile** |                            **True** | **330.5 ms** | **3.371 ms** |  **2.988 ms** | **35250.0000** | **1562.5000** | **1500.0000** | **215.82 MB** |
| FileHelpers |                            True | 122.6 ms | 4.127 ms | 12.038 ms | 22375.0000 | 1562.5000 | 1500.0000 | 157.84 MB |
