``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|    Method |        Mean |      Error |    StdDev |      Median |   Gen 0 |  Gen 1 | Allocated |
|---------- |------------:|-----------:|----------:|------------:|--------:|-------:|----------:|
|  FlatFile |    19.68 us |  0.3904 us |  1.095 us |    19.22 us |  3.3569 |      - |  15.59 KB |
| CsvHelper | 1,763.27 us | 17.8102 us | 15.788 us | 1,761.79 us | 15.6250 | 7.8125 |  74.03 KB |
