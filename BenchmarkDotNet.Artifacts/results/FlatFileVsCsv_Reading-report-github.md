``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|    Method |        Mean |      Error |    StdDev |      Median |   Gen 0 |  Gen 1 | Allocated |
|---------- |------------:|-----------:|----------:|------------:|--------:|-------:|----------:|
|  FlatFile |    23.66 us |  0.4899 us |  1.065 us |    23.16 us |  2.3499 |      - |  10.86 KB |
| CsvHelper | 1,102.29 us | 20.9492 us | 18.571 us | 1,094.74 us | 15.6250 | 1.9531 |   73.5 KB |
