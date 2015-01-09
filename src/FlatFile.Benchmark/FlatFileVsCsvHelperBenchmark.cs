namespace FlatFile.Benchmark
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using BenchmarkIt;
    using CsvHelper;
    using FlatFile.Benchmark.Entities;
    using FlatFile.Benchmark.Mapping;
    using FlatFile.Delimited.Implementation;
    using FluentAssertions;
    using Xunit;

    public class FlatFileVsCsvHelperBenchmark
    {
        [Fact]
        public void WriteAllRecordsWithMapping()
        {
            var records = new List<CustomObject>
            {
                new CustomObject
                {
                    GuidColumn = new Guid("f96a1c66-4777-4642-86fa-703098065f5f"),
                    IntColumn = 1,
                    StringColumn = "one",
                },
                new CustomObject
                {
                    GuidColumn = new Guid("06776ed9-d33f-470f-bd3f-8db842356330"),
                    IntColumn = 2,
                    StringColumn = "two",
                },
            };

            Benchmark.This("CsvWriter.WriteRecords", () =>
            {
                using (var memoryStream = new MemoryStream())
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var writer = new CsvWriter(streamWriter))
                {
                    writer.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();

                    writer.WriteRecords(records);

                    streamWriter.Flush();
                }
            })
                .Against.This("FlatFileEngine.Write", () =>
                {
                    var layout = new FlatFileMappingForCustomObject();
                    using (var stream = new MemoryStream())
                    {
                        var factory = new DelimitedFileEngineFactory();

                        var flatFile = factory.GetEngine<CustomObject>(layout);

                        flatFile.Write(stream, records);
                    }
                })
                .WithWarmup(1000)
                .For(10000)
                .Iterations()
                .PrintComparison();
        }

        [Fact(Skip = "Bug for GUID")]
        public void ReadAllRecordsWithMapping()
        {
            const string fileContent =
@"String Column,Int Column,Guid Column
one,1,f96a1c66-4777-4642-86fa-703098065f5f
two,2,06776ed9-d33f-470f-bd3f-8db842356330
";
            Benchmark.This("CsvWriter.WriteRecords", () =>
            {
                using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent)))
                using (var streamReader = new StreamReader(memoryStream))
                using (var reader = new CsvReader(streamReader))
                {
                    reader.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();

                    var objects = reader.GetRecords<CustomObject>().ToArray();

                    objects.Length.Should().Be(2);
                }
            })
                .Against.This("FlatFileEngine.Write", () =>
                {
                    var layout = new FlatFileMappingForCustomObject();
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent)))
                    {
                        var factory = new DelimitedFileEngineFactory();

                        var flatFile = factory.GetEngine<CustomObject>(layout);

                        var objects = flatFile.Read(stream).ToArray();

                        objects.Length.Should().Be(2);

                    }
                })
                .WithWarmup(1000)
                .For(10000)
                .Iterations()
                .PrintComparison();
        }
    }
}