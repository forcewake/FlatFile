namespace FlatFile.Benchmarks.Generators
{
    using FakeO;
    using FlatFile.Benchmarks.Entities;

    public class FakeGenarator
    {
        public FixedSampleRecord Generate(int next)
        {
            var record = Create.Fake<FixedSampleRecord>(
                c => c.Cuit = next,
                c => c.Nombre = String.Random(160),
                c => c.Actividad = Number.Next(100, 200));

            return record;
        }
    }
}