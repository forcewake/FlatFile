using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using CsvHelper;
using FluentFiles.Benchmark.Entities;
using FluentFiles.Benchmark.Mapping;
using FluentFiles.Core;
using FluentFiles.Delimited.Implementation;
using Configuration = CsvHelper.Configuration.Configuration;

namespace FluentFiles.Benchmark
{
    public class FluentFilesVsCsvHelperRead
    {
        private Configuration _csvConfig;
        private IFlatFileEngine _fluentEngine;

        private string _records;

        [Params(10, 100, 1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _csvConfig = new Configuration();
            _csvConfig.RegisterClassMap<CsvHelperMappingForCustomObject>();

            _fluentEngine = new DelimitedFileEngineFactory()
                .GetEngine(new FlatFileMappingForCustomObject());

            var records = new StringBuilder(N * 80);
            records.AppendLine("String Column,Int Column,Guid Column,Custom Type Column");
            for (int i = 0; i < N; i++)
            {
                records.AppendLine($"\"{i + 1}\",{i + 1},{Guid.NewGuid():D},{i + 1}|{i + 2}|{i + 3}");
            }

            _records = records.ToString();
        }

        [Benchmark(Baseline = true)]
        public IEnumerable<CustomObject> CsvHelper()
        {
            using (var streamReader = new StringReader(_records))
            using (var reader = new CsvReader(streamReader, _csvConfig))
            {
                return reader.GetRecords<CustomObject>().ToArray();
            }
        }

        [Benchmark]
        public IEnumerable<CustomObject> FluentFiles()
        {
            using (var streamReader = new StringReader(_records))
            {
                return _fluentEngine.Read<CustomObject>(streamReader).ToArray();
            }
        }
    }
}