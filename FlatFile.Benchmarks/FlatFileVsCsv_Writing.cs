namespace FlatFile.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using BenchmarkDotNet.Attributes;

    using CsvHelper;

    using FlatFile.Benchmarks.Entities;
    using FlatFile.Benchmarks.Mapping;
    using FlatFile.Delimited.Implementation;

    [MemoryDiagnoser]
    [Config(typeof(AllowNonOptimized))]
    public class FlatFileVsCsv_Writing
    {
        private List<CustomObject> records;

        [GlobalSetup]
        public void GlobalSetup()
        {
            records = new List<CustomObject>
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
        }

        [Benchmark]
        public void FlatFile()
        {
            var layout = new FlatFileMappingForCustomObject();
            using (var stream = new MemoryStream())
            {
                var factory = new DelimitedFileEngineFactory();
                var flatFile = factory.GetEngine(layout);
                flatFile.Write(stream, records);
            }
        }

        [Benchmark]
        public void CsvHelper()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var writer = new CsvWriter(streamWriter))
            {
                writer.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();
                writer.WriteRecords(records);
                streamWriter.Flush();
            }
        }
    }
}