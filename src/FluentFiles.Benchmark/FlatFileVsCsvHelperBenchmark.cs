namespace FluentFiles.Benchmark
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using BenchmarkIt;
    using CsvHelper;
    using FluentFiles.Benchmark.Entities;
    using FluentFiles.Benchmark.Mapping;
    using FluentFiles.Delimited.Implementation;
    using Xunit;

    public class FlatFileVsCsvHelperBenchmark
    {
        [Fact]
        [Trait("Category", "Benchmarks")]
        public void WriteAllRecordsWithMapping()
        {
            var records = new List<CustomObject>
            {
                new CustomObject
                {
                    GuidColumn = new Guid("f96a1c66-4777-4642-86fa-703098065f5f"),
                    IntColumn = 1,
                    StringColumn = "one",
                    CustomTypeColumn = new CustomType
                    {
                        First = 1,
                        Second = 2,
                        Third = 3,
                    },
                },
                new CustomObject
                {
                    GuidColumn = new Guid("06776ed9-d33f-470f-bd3f-8db842356330"),
                    IntColumn = 2,
                    StringColumn = "two",
                    CustomTypeColumn = new CustomType
                    {
                        First = 4,
                        Second = 5,
                        Third = 6,
                    },
                },
            };

            Benchmark
                .This("CsvWriter.WriteRecords", () =>
                {
                    using (var stream = new MemoryStream())
                    using (var streamWriter = new StreamWriter(stream))
                    using (var writer = new CsvWriter(streamWriter))
                    {
                        writer.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();

                        writer.WriteRecords(records);

                        streamWriter.Flush();
                    }
                })
                .Against
                .This("FlatFileEngine.Write", () =>
                {
                    var layout = new FlatFileMappingForCustomObject();
                    using (var stream = new MemoryStream())
                    using (var streamWriter = new StreamWriter(stream))
                    {
                        var factory = new DelimitedFileEngineFactory();
                        var engine = factory.GetEngine(layout);

                        engine.Write(streamWriter, records);
                    }
                })
                .WithWarmup(1000)
                .For(10000)
                .Iterations()
                .PrintComparison();
        }

        [Fact]
        [Trait("Category", "Benchmarks")]
        public void ReadAllRecordsWithMapping()
        {
            const string fileContent =
                @"String Column,Int Column,Guid Column,Custom Type Column
one,1,f96a1c66-4777-4642-86fa-703098065f5f,1|2|3
two,2,06776ed9-d33f-470f-bd3f-8db842356330,4|5|6
";
            Benchmark
                .This("CsvWriter.GetRecords", () =>
                {
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent)))
                    using (var streamReader = new StreamReader(stream))
                    using (var reader = new CsvReader(streamReader))
                    {
                        reader.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();

                        var objects = reader.GetRecords<CustomObject>().ToArray();
                    }
                })
                .Against
                .This("FlatFileEngine.Read", () =>
                {
                    var layout = new FlatFileMappingForCustomObject();
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent)))
                    using (var streamReader = new StreamReader(stream))
                    {
                        var factory = new DelimitedFileEngineFactory();
                        var engine = factory.GetEngine(layout);

                        var objects = engine.Read<CustomObject>(streamReader).ToArray();

                    }
                })
                .WithWarmup(1000)
                .For(10000)
                .Iterations()
                .PrintComparison();
        }
    }
}