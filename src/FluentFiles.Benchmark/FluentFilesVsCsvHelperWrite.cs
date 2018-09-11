using System.IO;
using System.Linq;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using CsvHelper;
using FluentFiles.Benchmark.Entities;
using FluentFiles.Benchmark.Mapping;
using FluentFiles.Delimited.Implementation;

namespace FluentFiles.Benchmark
{
    public class FluentFilesVsCsvHelperWrite
    {
        CustomObject[] _records;

        [Params(100, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            var fixture = new Fixture();
            _records = fixture.CreateMany<CustomObject>(N).ToArray();
        }

        [Benchmark(Baseline = true)]
        public void CsvHelper()
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            using (var writer = new CsvWriter(streamWriter))
            {
                writer.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();

                writer.WriteRecords(_records);

                streamWriter.Flush();
            }
        }

        [Benchmark]
        public void FluentFiles()
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                var factory = new DelimitedFileEngineFactory();
                var engine = factory.GetEngine(new FlatFileMappingForCustomObject());

                engine.Write(streamWriter, _records);
            }
        }
    }
}