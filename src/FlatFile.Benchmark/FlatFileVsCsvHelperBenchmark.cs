namespace FlatFile.Benchmark
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using BenchmarkIt;
    using CsvHelper;
    using FlatFile.Benchmark.Entities;
    using FlatFile.Benchmark.Mapping;
    using FlatFile.Delimited.Implementation;
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
    }
}