namespace FlatFile.Benchmarks
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;
    
    using FileHelpers;

    using FlatFile.Benchmarks.Entities;
    using FlatFile.Benchmarks.Generators;
    using FlatFile.Benchmarks.Mapping;
    using FlatFile.FixedLength.Implementation;

    using Hyper.ComponentModel;

    [MemoryDiagnoser]
    public class FlatFileVsFileHelpers_Writing
    {
        FakeGenarator genarator;
        FixedSampleRecord[] sampleRecords;

        [Params(true, false)]
        public bool useHyperTypeDescriptionProvider;

        [GlobalSetup]
        public void GlobalSetupAttribute()
        {
            genarator = new FakeGenarator();
            sampleRecords = Enumerable.Range(0, Program.iterations).Select(genarator.Generate).ToArray();
            if (useHyperTypeDescriptionProvider)
            {
                HyperTypeDescriptionProvider.Add(typeof(FixedSampleRecord));
            }
        }

        [Benchmark]
        public void FlatFile()
        {
            var layout = new FixedSampleRecordLayout();
            using (var stream = new MemoryStream())
            {
                var factory = new FixedLengthFileEngineFactory();
                var flatFile = factory.GetEngine(layout);
                flatFile.Write(stream, sampleRecords);
            }
        }

        [Benchmark]
        public void FileHelpers()
        {
            var engine = new FileHelperEngine<FixedSampleRecord>();
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                engine.WriteStream(streamWriter, sampleRecords);
            }
        }
    }
}