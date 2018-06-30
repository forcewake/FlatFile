``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |     Mean |     Error |   StdDev |   Median |      Gen 0 |      Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |---------:|----------:|---------:|---------:|-----------:|-----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **799.9 ms** | **14.746 ms** | **18.11 ms** | **799.3 ms** | **65937.5000** | **22250.0000** | **9437.5000** | **373.86 MB** |
| FileHelpers |                           False | 285.6 ms | 13.288 ms | 13.05 ms | 280.4 ms | 36750.0000 | 12875.0000 | 5750.0000 | 189.25 MB |
|    **FlatFile** |                            **True** | **803.0 ms** | **15.886 ms** | **15.60 ms** | **803.6 ms** | **65562.5000** | **21875.0000** | **9125.0000** | **373.86 MB** |
| FileHelpers |                            True | 286.9 ms |  5.984 ms | 10.32 ms | 282.1 ms | 36750.0000 | 12812.5000 | 5750.0000 | 189.25 MB |
