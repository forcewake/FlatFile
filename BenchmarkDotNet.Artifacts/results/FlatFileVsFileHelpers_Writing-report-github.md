``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |      Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **17.296 ms** | **0.1414 ms** | **0.1323 ms** | **4375.0000** | **1000.0000** | **1000.0000** |  **19.18 MB** |
| FileHelpers |                           False |  6.932 ms | 0.0292 ms | 0.0244 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.38 MB |
|    **FlatFile** |                            **True** | **17.250 ms** | **0.1956 ms** | **0.1634 ms** | **4375.0000** | **1000.0000** | **1000.0000** |  **19.18 MB** |
| FileHelpers |                            True |  6.961 ms | 0.0318 ms | 0.0297 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.39 MB |
