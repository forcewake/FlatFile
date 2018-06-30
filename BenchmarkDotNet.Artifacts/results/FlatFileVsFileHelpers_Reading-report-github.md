``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|      Method | useHyperTypeDescriptionProvider |      Mean |     Error |     StdDev |    Median |   Gen 0 |  Gen 1 | Allocated |
|------------ |-------------------------------- |----------:|----------:|-----------:|----------:|--------:|-------:|----------:|
|    **FlatFile** |                           **False** | **101.04 us** |  **7.742 us** |  **21.837 us** |  **94.25 us** | **11.5967** | **0.1221** |  **53.54 KB** |
| FileHelpers |                           False | 516.53 us | 53.082 us | 156.513 us | 520.84 us |  4.8828 | 2.4414 |  24.52 KB |
|    **FlatFile** |                            **True** |  **83.00 us** |  **2.366 us** |   **3.542 us** |  **81.30 us** | **11.5967** | **0.1221** |  **53.54 KB** |
| FileHelpers |                            True | 548.46 us | 68.278 us | 201.319 us | 575.73 us |  4.8828 | 2.4414 |  24.52 KB |
