``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.1.301
  [Host]     : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.1 (CoreCLR 4.6.26606.02, CoreFX 4.6.26606.05), 64bit RyuJIT


```
|    Method |     Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|---------- |---------:|----------:|----------:|----------:|----------:|---------:|----------:|
|  FlatFile | 84.22 ms | 0.5353 ms | 0.5007 ms | 4375.0000 | 1937.5000 | 937.5000 |  21.94 MB |
| CsvHelper | 73.04 ms | 0.5391 ms | 0.4779 ms | 6312.5000 | 1937.5000 | 937.5000 |  33.63 MB |
