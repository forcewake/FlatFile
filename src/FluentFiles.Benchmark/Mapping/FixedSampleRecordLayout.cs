namespace FluentFiles.Benchmark.Mapping
{
    using FluentFiles.Benchmark.Entities;
    using FluentFiles.FixedLength.Implementation;

    public sealed class FixedSampleRecordLayout : FixedLayout<FixedSampleRecord>
    {
        public FixedSampleRecordLayout()
        {
            this.WithMember(x => x.Id, c => c.WithLength(11))
                .WithMember(x => x.FirstName, c => c.WithLength(80))
                .WithMember(x => x.LastName, c => c.WithLength(80))
                .WithMember(x => x.Activity, c => c.WithLength(6));
        }
    }
}