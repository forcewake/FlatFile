using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace FluentFiles.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);

            var switcher = new BenchmarkSwitcher(typeof(Program).Assembly);
            switcher.Run(args, config);
        }
    }
}
