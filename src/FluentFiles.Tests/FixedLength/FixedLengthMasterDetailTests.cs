using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using FluentFiles.Core;
using FluentFiles.FixedLength;
using FluentFiles.FixedLength.Implementation;
using FluentAssertions;
using Xunit;
using System.Threading.Tasks;

namespace FluentFiles.Tests.FixedLength
{
    public class FixedLengthMasterDetailTests
    {
        private readonly IFlatFileMultiEngine engine;

        const string TestData = @"HHeader                     
MHeaderLine2                     
MHeaderLine3                     
D20150323Some Data                     
D20150512More Data                     
HAnotherHeader                     
D20150511FooBarBaz                     
SNonHeaderRecord                     
D20150512Standalone                     ";

        [Fact]
        public async Task ShouldAssociateDetailRecordsWithMasterRecord()
        {
            using (var reader = new StringReader(TestData))
                await engine.ReadAsync(reader);

            var headers = engine.GetRecords<HeaderRecord>().ToList();
            var continuations = engine.GetRecords<HeaderRecordContinuation>().ToList();

            var header1 = headers.FirstOrDefault(r => r.Data == "Header");
            header1.Should().NotBeNull("The first header record should exist");
            var header2 = continuations.FirstOrDefault(r => r.Data == "HeaderLine2");
            header2.Should().NotBeNull("The second header continuation record should exist");
            var header3 = continuations.FirstOrDefault(r => r.Data == "HeaderLine3");
            header3.Should().NotBeNull("The third header continuation record should exist");

            header1.DetailRecords.Should().BeNullOrEmpty("It does not have any detail records");
            header2.DetailRecords.Should().BeNullOrEmpty("It does not have any detail records");
            header3.DetailRecords.Should().HaveCount(2, "Two detail records exist");

            var detail = header3.DetailRecords[1] as DetailRecord;
            detail.Should().NotBeNull("It should be parsed correctly");
            detail.Data.Should().Be("20150512More Data", "It should preserve ordering");

            var anotherHeader = headers.FirstOrDefault(r => r.Data == "AnotherHeader");
            anotherHeader.Should().NotBeNull("The other header record should exist");
            anotherHeader.DetailRecords.Should().HaveCount(1, "One detail record exists");

            var anotherDetail = anotherHeader.DetailRecords[0] as DetailRecord;
            anotherDetail.Should().NotBeNull("It should be parsed correctly");
            anotherDetail.Data.Should().Be("20150511FooBarBaz", "It should associate the correct record");

            engine.GetRecords<DetailRecord>().Should().HaveCount(1, "Only unassociated detail records should be available when calling GetRecords<T>");
        }

        abstract class RecordBase
        {
            public char Type { get; set; }
            public string Data { get; set; }

            bool Equals(RecordBase other) { return Type == other.Type && string.Equals(Data, other.Data); }

            public override int GetHashCode() => HashCode.Combine(Type, Data);

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((RecordBase)obj);
            }
        }

        class HeaderRecord : RecordBase, IMasterRecord
        {
            public IList<IDetailRecord> DetailRecords { get; protected set; }
            public HeaderRecord()
            {
                Type = 'H';
                DetailRecords = new List<IDetailRecord>();
            }
        }

        class HeaderRecordContinuation : HeaderRecord
        {
            public HeaderRecordContinuation()
            {
                Type = 'M';
                DetailRecords = new List<IDetailRecord>();
            }
        }

        class DetailRecord : RecordBase, IDetailRecord
        {
            public DetailRecord() { Type = 'D'; }
        }

        class StandaloneRecord : RecordBase
        {
            public StandaloneRecord() { Type = 'S'; }
        }

        abstract class RecordBaseLayout<T> : FixedLayout<T> where T : RecordBase
        {
            protected RecordBaseLayout()
            {
                this.WithMember(x => x.Type, c => c.WithLength(1))
                    .WithMember(x => x.Data, c => c.WithLength(20).WithRightPadding(' '));
            }
        }

        class HeaderLayout : RecordBaseLayout<HeaderRecord> { }

        class HeaderContinuationLayout : RecordBaseLayout<HeaderRecordContinuation> { }

        class DetailLayout : RecordBaseLayout<DetailRecord> { }

        class StandaloneLayout : RecordBaseLayout<StandaloneRecord> { }

        public FixedLengthMasterDetailTests()
        {
            var layouts = new List<IFixedLengthLayoutDescriptor>
            {
                new HeaderLayout(),
                new HeaderContinuationLayout(),
                new DetailLayout(),
                new StandaloneLayout()
            };
            engine = new FixedLengthFileMultiEngine(layouts,
                                                    (s, i) =>
                                                    {
                                                        if (String.IsNullOrEmpty(s) || s.Length < 1) return null;

                                                        switch (s[0])
                                                        {
                                                            case 'H':
                                                                return typeof (HeaderRecord);
                                                            case 'M':
                                                                return typeof (HeaderRecordContinuation);
                                                            case 'S':
                                                                return typeof (StandaloneRecord);
                                                            case 'D':
                                                                return typeof (DetailRecord);
                                                            default:
                                                                return null;
                                                        }
                                                    },
                                                    new FixedLengthLineBuilderFactory(),
                                                    new FixedLengthLineParserFactory(),
                                                    new DefaultFixedLengthMasterDetailStrategy());
        }
    }
}