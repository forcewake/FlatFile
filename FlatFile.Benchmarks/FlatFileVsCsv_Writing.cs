namespace FlatFile.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    
    using CsvHelper;

    using FlatFile.Benchmarks.Entities;
    using FlatFile.Benchmarks.Mapping;
    using FlatFile.Delimited.Implementation;

    [MemoryDiagnoser]
    public class FlatFileVsCsv_Writing
    {
        private List<CustomObject> records;

        [GlobalSetup]
        public void GlobalSetup()
        {
            records = new List<CustomObject>();
            for (var x = 0; x < Program.iterations; x++)
            {
                records.Add(new CustomObject
                {
                    GuidColumn = Guid.NewGuid(),
                    IntColumn = 1,
                    StringColumn = "one",
                    CustomTypeColumn = new CustomType
                    {
                        First = 1,
                        Second = 2,
                        Third = 3,
                    },
                });
            }
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