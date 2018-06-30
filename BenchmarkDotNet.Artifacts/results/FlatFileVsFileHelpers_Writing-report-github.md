``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |      Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **18.860 ms** | **0.3742 ms** | **0.5122 ms** | **7875.0000** | **1125.0000** | **1000.0000** |  **34.86 MB** |
| FileHelpers |                           False |  7.604 ms | 0.1501 ms | 0.1952 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.39 MB |
|    **FlatFile** |                            **True** | **19.216 ms** | **0.2678 ms** | **0.2374 ms** | **7875.0000** | **1125.0000** | **1000.0000** |  **34.86 MB** |
| FileHelpers |                            True |  7.874 ms | 0.1472 ms | 0.1446 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.39 MB |
