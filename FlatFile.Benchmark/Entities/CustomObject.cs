namespace FlatFile.Benchmark.Entities
{
    using System;
    using Newtonsoft.Json;

    public class CustomObject
    {
        public Guid GuidColumn { get; set; }

        public int IntColumn { get; set; }

        public string StringColumn { get; set; }

        public string IgnoredColumn { get; set; }

        public CustomType CustomTypeColumn { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}