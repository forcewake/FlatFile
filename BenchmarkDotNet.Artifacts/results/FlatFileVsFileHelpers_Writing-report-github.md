``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |      Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **18.120 ms** | **0.2515 ms** | **0.2353 ms** | **5875.0000** | **1093.7500** | **1000.0000** |  **25.94 MB** |
| FileHelpers |                           False |  7.196 ms | 0.0832 ms | 0.0737 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.39 MB |
|    **FlatFile** |                            **True** | **17.497 ms** | **0.1677 ms** | **0.1400 ms** | **5875.0000** | **1093.7500** | **1000.0000** |  **25.94 MB** |
| FileHelpers |                            True |  7.555 ms | 0.1259 ms | 0.1178 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.39 MB |
