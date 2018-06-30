``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|    Method |     Mean |     Error |    StdDev |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|---------- |---------:|----------:|----------:|----------:|---------:|---------:|----------:|
|  FlatFile | 17.68 ms | 0.0434 ms | 0.0406 ms | 3875.0000 | 593.7500 | 500.0000 |  17.42 MB |
| CsvHelper | 34.51 ms | 0.1284 ms | 0.1072 ms | 4312.5000 | 250.0000 | 250.0000 |   20.3 MB |
