``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |       Mean |     Error |    StdDev |      Gen 0 |      Gen 1 |     Gen 2 | Allocated |
|------------ |-------------------------------- |-----------:|----------:|----------:|-----------:|-----------:|----------:|----------:|
|    **FlatFile** |                           **False** | **1,079.3 ms** | **21.526 ms** | **27.223 ms** | **80125.0000** | **25437.5000** | **9125.0000** | **460.83 MB** |
| FileHelpers |                           False |   334.6 ms |  7.107 ms |  8.988 ms | 36750.0000 | 12812.5000 | 5750.0000 | 189.25 MB |
|    **FlatFile** |                            **True** |   **991.1 ms** |  **4.483 ms** |  **2.965 ms** | **80562.5000** | **26125.0000** | **9562.5000** | **460.83 MB** |
| FileHelpers |                            True |   297.5 ms |  1.793 ms |  1.677 ms | 36750.0000 | 12812.5000 | 5750.0000 | 189.25 MB |
