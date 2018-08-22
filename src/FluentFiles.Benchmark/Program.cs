using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System.IO;
using System.Reflection;

namespace FluentFiles.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
                                     .WithArtifactsPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            var switcher = new BenchmarkSwitcher(typeof(Program).Assembly);
            switcher.Run(args, config);
        }
    }
}
