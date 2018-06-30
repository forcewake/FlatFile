``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |      Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **11.142 ms** | **0.2170 ms** | **0.2030 ms** | **3375.0000** | **1062.5000** | **1000.0000** |  **14.61 MB** |
| FileHelpers |                           False |  7.643 ms | 0.1528 ms | 0.2467 ms | 3109.3750 | 1007.8125 | 1000.0000 |  13.39 MB |
|    **FlatFile** |                            **True** | **11.221 ms** | **0.2049 ms** | **0.1917 ms** | **3375.0000** | **1062.5000** | **1000.0000** |  **14.61 MB** |
| FileHelpers |                            True |  7.811 ms | 0.1548 ms | 0.2317 ms | 3109.3750 | 1015.6250 | 1000.0000 |  13.38 MB |
