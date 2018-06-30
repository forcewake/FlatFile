﻿namespace FlatFile.Benchmarks
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using BenchmarkDotNet.Attributes;
    
    using CsvHelper;

    using FlatFile.Benchmarks.Entities;
    using FlatFile.Benchmarks.Mapping;
    using FlatFile.Delimited.Implementation;

    [MemoryDiagnoser]
    public class FlatFileVsCsv_Reading
    {
        const string fileHeader = "String Column,Int Column,Guid Column,Custom Type Column";
        const string fileContent = @"one,1,f96a1c66-4777-4642-86fa-703098065f5f,1|2|3
two,2,06776ed9-d33f-470f-bd3f-8db842356330,4|5|6
";
        private string sampleText;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var sb = new StringBuilder();
            sb.AppendLine(fileHeader);
            for (var x = 0; x < Program.iterations; x++ )
            {
                sb.AppendLine(fileContent);
            }
            sampleText = sb.ToString();
        }

        [Benchmark]
        public void FlatFile()
        {
            var layout = new FlatFileMappingForCustomObject();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText)))
            {
                var factory = new DelimitedFileEngineFactory();
                var flatFile = factory.GetEngine(layout);
                var objects = flatFile.Read<CustomObject>(stream).ToArray();
            }
        }

        [Benchmark]
        public void CsvHelper()
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText)))
            using (var streamReader = new StreamReader(memoryStream))
            using (var reader = new CsvReader(streamReader))
            {
                reader.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();
                var objects = reader.GetRecords<CustomObject>().ToArray();
            }
        }
    }
}