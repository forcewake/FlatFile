namespace FluentFiles.Benchmark.Entities
{
    using Newtonsoft.Json;

    public class CustomType
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Third { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
