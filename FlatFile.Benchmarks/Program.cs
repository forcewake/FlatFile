
namespace FlatFile.Benchmarks
{
    using System;
    using System.Linq;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;
    using BenchmarkDotNet.Validators;

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Starting benchmarks");
            var config = new AllowNonOptimized();
            var reading_vs_Csv = BenchmarkRunner.Run<FlatFileVsCsv_Reading>(config);
            var writing_vs_Csv = BenchmarkRunner.Run<FlatFileVsCsv_Writing>(config);
            var reading_vs_FlatFile = BenchmarkRunner.Run<FlatFileVsFileHelpers_Reading>(config);
            var reawriting_vs_FlatFile = BenchmarkRunner.Run<FlatFileVsFileHelpers_Writing>(config);
             Console.WriteLine("Finished benchmarks");
        }
    }

    public class AllowNonOptimized : ManualConfig
    {
        public AllowNonOptimized()
        {
            Add(JitOptimizationsValidator.DontFailOnError); // ALLOW NON-OPTIMIZED DLLS

            Add(DefaultConfig.Instance.GetLoggers().ToArray()); // manual config has no loggers by default
            Add(DefaultConfig.Instance.GetExporters().ToArray()); // manual config has no exporters by default
            Add(DefaultConfig.Instance.GetColumnProviders().ToArray()); // manual config has no columns by default
        }
    }
}
