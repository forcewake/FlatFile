using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using CsvHelper;
using FluentFiles.Benchmark.Entities;
using FluentFiles.Benchmark.Mapping;
using FluentFiles.Delimited.Implementation;

namespace FluentFiles.Benchmark
{
    public class FluentFilesVsCsvHelperRead
    {
        const string Content =
@"String Column,Int Column,Guid Column,Custom Type Column
one,1,f96a1c66-4777-4642-86fa-703098065f5f,1|2|3
two,2,06776ed9-d33f-470f-bd3f-8db842356330,4|5|6
three,3,ed3085c8-354f-4068-bd73-499b73748189,7|8|9
four,4,951ca0e2-3cbd-49bf-a1bb-cd2591869cb7,10|11|12
";

        [Benchmark(Baseline = true)]
        public IEnumerable<CustomObject> CsvHelper()
        {
            using (var streamReader = new StringReader(Content))
            using (var reader = new CsvReader(streamReader))
            {
                reader.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();

                return reader.GetRecords<CustomObject>().ToArray();
            }
        }

        [Benchmark]
        public IEnumerable<CustomObject> FluentFiles()
        {
            using (var streamReader = new StringReader(Content))
            {
                var factory = new DelimitedFileEngineFactory();
                var engine = factory.GetEngine(new FlatFileMappingForCustomObject());

                return engine.Read<CustomObject>(streamReader).ToArray();
            }
        }
    }
}