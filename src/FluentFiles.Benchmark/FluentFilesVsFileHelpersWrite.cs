using System.IO;
using System.Linq;
using FileHelpers;
using FluentFiles.Benchmark.Entities;
using FluentFiles.Benchmark.Mapping;
using FluentFiles.FixedLength.Implementation;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using FluentFiles.Core;
using FluentFiles.Converters;

namespace FluentFiles.Benchmark
{
    public class FluentFilesVsFileHelpersWrite
    {
        private FileHelperEngine<FixedSampleRecord> _helperEngine;
        private IFlatFileEngine _fluentEngine;

        private FixedSampleRecord[] _records;

        [Params(10, 100, 1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _helperEngine = new FileHelperEngine<FixedSampleRecord>();

            var factory = new FixedLengthFileEngineFactory();
            _fluentEngine = factory.GetEngine(new FixedSampleRecordLayout());

            Configuration.Converters.UseOptimizedConverters();

            var fixture = new Fixture();
            _records = fixture.CreateMany<FixedSampleRecord>(N).ToArray();
        }

        [Benchmark(Baseline = true)]
        public void FileHelpers()
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                _helperEngine.WriteStream(streamWriter, _records);
            }
        }

        [Benchmark]
        public void FluentFiles()
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                _fluentEngine.Write(streamWriter, _records);
            }
        }
    }
}