using System.Linq;
using BenchmarkDotNet.Attributes;
using FluentFiles.Core.Conversion;

namespace FluentFiles.Benchmark
{
    public class Int32ConverterBenchmarks
    {
        private System.ComponentModel.Int32Converter _bclConverter = new System.ComponentModel.Int32Converter();
        private FluentFiles.Converters.Int32Converter _spannifiedConverter = new FluentFiles.Converters.Int32Converter();

        private string[] items;

        [Params(10, 100, 1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            items = Enumerable.Range(1, N).Select(n => n.ToString()).ToArray();
        }

        [Benchmark(Baseline = true)]
        public int[] BCL()
        {
            return items.Select(i => _bclConverter.ConvertFromString(i)).Cast<int>().ToArray();
        }

        [Benchmark]
        public int[] Spannified()
        {
            return items.Select(i => _spannifiedConverter.Parse(new FieldParsingContext(i, null))).Cast<int>().ToArray();
        }
    }
}