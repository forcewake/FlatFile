using System.IO;
using System.Linq;
using FileHelpers;
using FluentFiles.Benchmark.Entities;
using FluentFiles.Benchmark.Mapping;
using FluentFiles.FixedLength.Implementation;
using Hyper.ComponentModel;
using AutoFixture;
using BenchmarkDotNet.Attributes;

namespace FluentFiles.Benchmark
{
    public class FluentFilesVsFileHelpersWrite
    {
        FixedSampleRecord[] _records;

        [Params(100, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            var fixture = new Fixture();
            _records = fixture.CreateMany<FixedSampleRecord>(N).ToArray();
        }

        [Benchmark]
        public void FileHelpers()
        {
            var engine = new FileHelperEngine<FixedSampleRecord>();
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                engine.WriteStream(streamWriter, _records);
            }
        }

        [Benchmark]
        public void FluentFiles()
        {
            var factory = new FixedLengthFileEngineFactory();
            var engine = factory.GetEngine(new FixedSampleRecordLayout());
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                engine.Write(streamWriter, _records);
            }
        }

        [Benchmark]
        public void FileHelpers_FastReflection()
        {
            HyperTypeDescriptionProvider.Add(typeof(FixedSampleRecord));
            FileHelpers();
        }

        [Benchmark]
        public void FluentFiles_FastReflection()
        {
            HyperTypeDescriptionProvider.Add(typeof(FixedSampleRecord));
            FluentFiles();
        }
    }
}