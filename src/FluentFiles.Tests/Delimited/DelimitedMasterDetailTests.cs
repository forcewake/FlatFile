using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentFiles.Core;
using FluentFiles.Delimited;
using FluentFiles.Delimited.Implementation;
using FluentAssertions;
using Xunit;
using System.Threading.Tasks;

namespace FluentFiles.Tests.Delimited
{
    public class DelimitedMasterDetailTests
    {
        private readonly DelimitedFileEngineFactory _factory = new DelimitedFileEngineFactory();
        private readonly IFlatFileMultiEngine _engine;

        const string TestData =
@"M,First Master,00042
D,20150323,First Detail
D,20160323,Second Detail
M,Second Master,00044";

        public DelimitedMasterDetailTests()
        {
            _engine = _factory.GetEngine(new IDelimitedLayoutDescriptor[] {new MasterRecordLayout(), new DetailRecordLayout() },
                                        (line, number) =>
                                        {
                                            if (String.IsNullOrEmpty(line) || line.Length < 1) return null;

                                            switch (line[0])
                                            {
                                                case 'M':
                                                    return typeof(MasterRecord);
                                                case 'D':
                                                    return typeof(DetailRecord);
                                                default:
                                                    return null;
                                            }
                                        });
        }

        [Fact]
        public async Task ShouldAssociateDetailRecordsWithMasterRecord()
        {
            // Act.
            using (var reader = new StringReader(TestData))
                await _engine.ReadAsync(reader);

            // Assert.
            var results = _engine.GetRecords<MasterRecord>().ToList();
            results.Should().HaveCount(2, "because it should read two 'M' records");
            results.First().Should().Be(new MasterRecord { Type = 'M', Description = "First Master", Value = 42 });

            var details = results.First().DetailRecords;
            details.Should().HaveCount(2, "because there are two 'D' records");
            details.First().Should().Be(new DetailRecord { Type = 'D', Description = "First Detail", Date = "20150323" });
            details.Last().Should().Be(new DetailRecord { Type = 'D', Description = "Second Detail", Date = "20160323" });

            results.Last().Should().Be(new MasterRecord { Type = 'M', Description = "Second Master", Value = 44 });
        }

        class MasterRecord : IMasterRecord
        {
            public IList<IDetailRecord> DetailRecords { get; } = new List<IDetailRecord>();

            public char Type { get; set; }

            public string Description { get; set; }

            public int Value { get; set; }

            bool Equals(MasterRecord other) { return Type == other.Type && string.Equals(Description, other.Description) && Value == other.Value; }

            public override int GetHashCode() => HashCode.Combine(Type, Description, Value);

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((MasterRecord)obj);
            }
        }

        class DetailRecord : IDetailRecord
        {
            public char Type { get; set; }

            public string Date { get; set; }

            public string Description { get; set; }

            bool Equals(DetailRecord other) { return Type == other.Type && string.Equals(Date, other.Date) && string.Equals(Description, other.Description); }

            public override int GetHashCode() => HashCode.Combine(Type, Date, Description);

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((DetailRecord)obj);
            }
        }

        sealed class MasterRecordLayout : DelimitedLayout<MasterRecord>
        {
            public MasterRecordLayout()
            {
                HasHeader = false;
                this.WithDelimiter(",")
                    .WithMember(x => x.Type)
                    .WithMember(x => x.Description)
                    .WithMember(x => x.Value);
            }
        }

        sealed class DetailRecordLayout : DelimitedLayout<DetailRecord>
        {
            public DetailRecordLayout()
            {
                this.WithDelimiter(",")
                    .WithMember(x => x.Type)
                    .WithMember(x => x.Date)
                    .WithMember(x => x.Description);
            }
        }
    }
}