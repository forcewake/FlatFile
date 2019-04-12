using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentFiles.Core;
using FluentFiles.FixedLength;
using FluentFiles.FixedLength.Implementation;
using FluentAssertions;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

namespace FluentFiles.Tests.FixedLength
{
    public class FixedLengthMultiEngineTests
    {
        const string TestData =
@"Some Header To Skip
STest Description    00042
D20150323Another Description ";

        private FixedLengthFileMultiEngine CreateEngine(Func<Func<string, int, Type>, string, int, Type> interceptor = null)
        {
            if (interceptor == null)
                interceptor = (f, l, n) => f(l, n);

            return new FixedLengthFileMultiEngine(new IFixedLengthLayoutDescriptor[] { new Record1Layout(), new Record2Layout() },
                                                    (line, number) => interceptor((l, n) =>
                                                    {
                                                        if (String.IsNullOrEmpty(l) || l.Length < 1) return null;

                                                        switch (l[0])
                                                        {
                                                            case 'S':
                                                                return typeof(Record1);
                                                            case 'D':
                                                                return typeof(Record2);
                                                            default:
                                                                return null;
                                                        }
                                                    }, line, number),
                                                    new FixedLengthLineBuilderFactory(),
                                                    new FixedLengthLineParserFactory(),
                                                    new DefaultFixedLengthMasterDetailStrategy())
            { HasHeader = true };
        }

        [Fact]
        public async Task EngineShouldReadMultipleRecordTypes()
        {
            var engine = CreateEngine();

            using (var reader = new StringReader(TestData))
                await engine.ReadAsync(reader);

            var record1Results = engine.GetRecords<Record1>().ToList();
            var record2Results = engine.GetRecords<Record2>().ToList();

            record1Results.Should().HaveCount(1, "because it should skip the header and read one 'S' record");
            record2Results.Should().HaveCount(1, "because there is one 'D' record");

            record1Results.Single().Should().Be(new Record1 { Description = "Test Description", Value = 42 });
            record2Results.Single().Should().Be(new Record2 { Description = "Another Description", Date = "20150323" });
        }

        [Fact]
        public async Task ReadShouldStopOnCancellation()
        {
            // Arrange.
            var tcs = new CancellationTokenSource();

            var engine = CreateEngine((f, line, number) =>
            {
                tcs.Cancel();
                return f(line, number);
            });

            // Act.
            using (var reader = new StringReader(TestData))
                await Assert.ThrowsAsync<OperationCanceledException>(() => engine.ReadAsync(reader, tcs.Token));

            // Assert.
            var record1Results = engine.GetRecords<Record1>().ToList();
            var record2Results = engine.GetRecords<Record2>().ToList();

            record1Results.Should().HaveCount(1);
            record2Results.Should().BeEmpty();

            record1Results.Single().Should().Be(new Record1 { Description = "Test Description", Value = 42 });
        }

        class Record1
        {
            public char Type { get; set; }
            public string Description;
            public int Value { get; set; }

            public Record1() { Type = 'S'; }

            bool Equals(Record1 other) { return Type == other.Type && string.Equals(Description, other.Description) && Value == other.Value; }

            public override int GetHashCode() => HashCode.Combine(Type, Description, Value);

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((Record1) obj);
            }
        }

        class Record2
        {
            public char Type { get; set; }
            public string Date { get; set; }
            public string Description;

            public Record2() { Type = 'D'; }

            bool Equals(Record2 other) { return Type == other.Type && string.Equals(Date, other.Date) && string.Equals(Description, other.Description); }

            public override int GetHashCode() => HashCode.Combine(Type, Date, Description);

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((Record2) obj);
            }
        }

        sealed class Record1Layout : FixedLayout<Record1>
        {
            public Record1Layout()
            {
                this.WithMember(x => x.Type, c => c.WithLength(1))
                    .WithMember(x => x.Description, c => c.WithLength(20).WithRightPadding(' '))
                    .WithMember(x => x.Value, c => c.WithLength(5).WithLeftPadding('0'));
            }
        }

        sealed class Record2Layout : FixedLayout<Record2>
        {
            public Record2Layout()
            {
                this.WithMember(x => x.Type, c => c.WithLength(1))
                    .WithMember(x => x.Date, c => c.WithLength(8))
                    .WithMember(x => x.Description, c => c.WithLength(20).WithRightPadding(' '));
            }
        }
    }
}