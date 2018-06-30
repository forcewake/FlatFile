``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |     Mean |     Error |    StdDev |      Gen 0 |      Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |---------:|----------:|----------:|-----------:|-----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **798.8 ms** | **15.719 ms** | **22.036 ms** | **63562.5000** | **20625.0000** | **7187.5000** | **373.86 MB** |
| FileHelpers |                           False | 285.0 ms |  5.697 ms |  6.561 ms | 36750.0000 | 12812.5000 | 5750.0000 | 189.25 MB |
|    **FlatFile** |                            **True** | **773.0 ms** | **15.073 ms** | **16.128 ms** | **65750.0000** | **22062.5000** | **9312.5000** | **373.86 MB** |
| FileHelpers |                            True | 277.8 ms |  1.703 ms |  1.593 ms | 36750.0000 | 12812.5000 | 5750.0000 | 189.25 MB |
