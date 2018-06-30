``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |      Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **19.646 ms** | **0.3781 ms** | **0.4045 ms** | **8812.5000** | **1187.5000** | **1000.0000** |  **39.06 MB** |
| FileHelpers |                           False |  7.902 ms | 0.1526 ms | 0.1817 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.39 MB |
|    **FlatFile** |                            **True** | **19.560 ms** | **0.2957 ms** | **0.2621 ms** | **8812.5000** | **1187.5000** | **1000.0000** |  **39.06 MB** |
| FileHelpers |                            True |  7.859 ms | 0.1494 ms | 0.1467 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.39 MB |
