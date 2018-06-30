``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |     Mean |     Error |    StdDev |      Gen 0 |      Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |---------:|----------:|----------:|-----------:|-----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **778.4 ms** | **15.389 ms** | **14.395 ms** | **62687.5000** | **20312.5000** | **6375.0000** | **373.86 MB** |
| FileHelpers |                           False | 287.6 ms |  2.008 ms |  1.878 ms | 36750.0000 | 12812.5000 | 5750.0000 | 189.25 MB |
|    **FlatFile** |                            **True** | **785.4 ms** | **15.158 ms** | **15.566 ms** | **65687.5000** | **22000.0000** | **9250.0000** | **373.86 MB** |
| FileHelpers |                            True | 284.4 ms |  1.447 ms |  1.353 ms | 36750.0000 | 12875.0000 | 5750.0000 | 189.25 MB |
