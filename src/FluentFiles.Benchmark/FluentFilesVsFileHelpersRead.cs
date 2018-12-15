using AutoFixture;
using BenchmarkDotNet.Attributes;
using FileHelpers;
using FluentFiles.Benchmark.Entities;
using FluentFiles.Benchmark.Mapping;
using FluentFiles.Converters;
using FluentFiles.Core;
using FluentFiles.FixedLength.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FluentFiles.Benchmark
{
    public class FluentFilesVsFileHelpersRead
    {
        private FileHelperEngine<FixedSampleRecord> _helperEngine;
        private IFlatFileEngine _fluentEngine;

        private string _records;

        [Params(10, 100, 1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _helperEngine = new FileHelperEngine<FixedSampleRecord>();

            _fluentEngine = new FixedLengthFileEngineFactory()
                .GetEngine(new FixedSampleRecordLayout());

            Configuration.Converters.UseOptimizedConverters();

            var records = new StringBuilder(N * 185);
            var random = new Random();
            var fixture = new Fixture().Customize(new RandomFixedStringCustomization(80));
            for (int i = 0; i < N; i++)
            {
                records.Append(20000000000L + i)
                       .Append(fixture.Create<string>())
                       .Append(fixture.Create<string>())
                       .Append(random.Next(0, 999999).ToString().PadRight(6))
                       .AppendLine();
            }

            _records = records.ToString();
        }

        [Benchmark(Baseline = true)]
        public IEnumerable<FixedSampleRecord> FileHelpers()
        {
            using (var stream = new StringReader(_records))
            {
                return _helperEngine.ReadStream(stream);
            }
        }

        [Benchmark]
        public IEnumerable<FixedSampleRecord> FluentFiles()
        {
            using (var stream = new StringReader(_records))
            {
                return _fluentEngine.Read<FixedSampleRecord>(stream).ToArray();
            }
        }
    }

    class RandomFixedStringCustomization : ICustomization
    {
        private readonly Random _random = new Random();
        private readonly int _maxLength;

        public RandomFixedStringCustomization(int maxLength)
        {
            _maxLength = maxLength;
        }

        public void Customize(IFixture fixture)
        {
            fixture.RepeatCount = _maxLength;
            fixture.Customize<string>(c => c.FromFactory(() => 
                new string(fixture.CreateMany<char>()
                                  .Take(_random.Next(1, _maxLength + 1))
                                  .ToArray()).PadRight(_maxLength)));
        }
    }
}