``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|    Method |     Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|---------- |---------:|----------:|----------:|----------:|----------:|---------:|----------:|
|  FlatFile | 86.41 ms | 1.7182 ms | 2.0454 ms | 4375.0000 | 1937.5000 | 937.5000 |  21.94 MB |
| CsvHelper | 74.51 ms | 0.7201 ms | 0.6014 ms | 6312.5000 | 1937.5000 | 937.5000 |  33.63 MB |
