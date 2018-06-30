``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|    Method |     Mean |     Error |    StdDev |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|---------- |---------:|----------:|----------:|----------:|---------:|---------:|----------:|
|  FlatFile | 18.61 ms | 0.1575 ms | 0.1397 ms | 4812.5000 | 656.2500 | 500.0000 |  21.46 MB |
| CsvHelper | 33.94 ms | 0.2184 ms | 0.2043 ms | 4312.5000 | 250.0000 | 250.0000 |   20.3 MB |
