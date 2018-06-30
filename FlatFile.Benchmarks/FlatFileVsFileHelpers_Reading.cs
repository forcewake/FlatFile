namespace FlatFile.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;

    using FileHelpers;

    using FlatFile.Benchmarks.Entities;
    using FlatFile.Benchmarks.Mapping;
    using FlatFile.FixedLength.Implementation;

    [MemoryDiagnoser]
    public class FlatFileVsFileHelpers_Reading
    {
        const string FixedFileSample =
            @"20000000109PANIAGUA JOSE                                                                                                                                                   0     
20000000125ACOSTA MARCOS                                                                                                                                                   0     
20000000141GONZALEZ DOMINGO                                                                                                                                                0     
20000000168SENA RAUL                                                                                                                                                       0     
20000000192BITTAR RUSTON MUSA                                                                                                                                              0     
20000000206CORDON SERGIO ALFREDO                                                                                                                                           0     
20000000222CAVAGNARO ERNESTO RODAS GUILLERMO Y RIV                                                                                                                         0     
20000000338BRUCE LUIS                                                                                                                                                      0     
20000000354CHAVEZ DAMIAN JOSE                                                                                                                                              0     
20000000389ZINGARETTI SANTIAGO ENRIQUE ARDUINO                                                                                                                             0     
20000000400PEREYRA ARNOTI LEONARDO ANDRES                                                                                                                                  0     
20000000516VELAZQUEZ ANIBAL ARISTOBU                                                                                                                                       0     
20000000532GONZALEZ DOMINGO                                                                                                                                                0     
20000000613CANALE S A CTA RECAUDADORA                                                                                                                                      0     
20000000656MU#OZ FERNANDEZ ALEJANDRO                                                                                                                                       0     
20000000745FERNANDEZ MONTOYA CHARLES JAIME                                                                                                                                 0     
20000000869MOSCHION DANILO JUAN                                                                                                                                            602290
20000000885CHOQUE RAMON FELIX                                                                                                                                              0     
20000000923AQUINO VILLASANTI NICASIO                                                                                                                                       0     
";


        [GlobalSetup]
        public void GlobalSetupAttribute()
        {
            sampleText = String.Join("", Enumerable.Repeat(FixedFileSample, Program.iterations));
            Console.WriteLine($"// Benchmark input is {sampleText.Length} characters or {Encoding.Default.GetByteCount(sampleText)} bytes.");
        }

        [Params(true, false)]
        public bool useHyperTypeDescriptionProvider;
        private string sampleText;

        [Benchmark]
        public void FlatFile()
        {
            var layout = new FixedSampleRecordLayout();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sampleText)))
            {
                var factory = new FixedLengthFileEngineFactory();
                var flatFile = factory.GetEngine(layout);
                var records = flatFile.Read<FixedSampleRecord>(stream).ToArray();
            }
        }

        [Benchmark]
        public void FileHelpers()
        {
            var engine = new FileHelperEngine<FixedSampleRecord>();
            using (var stream = new StringReader(sampleText))
            {
                var records = engine.ReadStream(stream);
            }
        }
    }
}