using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentFiles.Core;
using FluentFiles.Delimited;
using FluentFiles.Delimited.Attributes;
using FluentFiles.Delimited.Implementation;
using FluentAssertions;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

namespace FluentFiles.Tests.Delimited
{
    public class DelimitedMultiEngineTests
    {
        private readonly DelimitedFileEngineFactory _factory = new DelimitedFileEngineFactory();

        const string TestData =
@"S,Test Description,00042
D,20150323,Another Description";

        private IFlatFileMultiEngine CreateEngine(Func<Func<string, int, Type>, string, int, Type> interceptor = null)
        {
            if (interceptor == null)
                interceptor = (f, l, n) => f(l, n);

            return _factory.GetEngine(new[] { typeof(Record1), typeof(Record2) },
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
                                        }, line, number));
        }

        [Fact]
        public async Task EngineShouldReadMultipleRecordTypes()
        {
            // Arrange.
            var engine = CreateEngine();

            // Act.
            using (var reader = new StringReader(TestData))
                await engine.ReadAsync(reader);

            // Assert.
            var record1Results = engine.GetRecords<Record1>().ToList();
            var record2Results = engine.GetRecords<Record2>().ToList();

            record1Results.Should().HaveCount(1, "because it should read one 'S' record");
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

        [DelimitedFile(Delimiter = ",", HasHeader = false)]
        class Record1
        {
            [DelimitedField(1)]
            public char Type { get; set; }

            [DelimitedField(2)]
            public string Description { get; set; }

            [DelimitedField(3)]
            public int Value { get; set; }

            public Record1() { Type = 'S'; }

            bool Equals(Record1 other) { return Type == other.Type && string.Equals(Description, other.Description) && Value == other.Value; }

            public override int GetHashCode() => HashCode.Combine(Type, Description, Value);

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((Record1)obj);
            }
        }

        [DelimitedFile(Delimiter = ",", HasHeader = false)]
        class Record2
        {

            [DelimitedField(1)]
            public char Type { get; set; }

            [DelimitedField(2)]
            public string Date { get; set; }

            [DelimitedField(3)]
            public string Description { get; set; }

            public Record2() { Type = 'D'; }

            bool Equals(Record2 other) { return Type == other.Type && string.Equals(Date, other.Date) && string.Equals(Description, other.Description); }

            public override int GetHashCode() => HashCode.Combine(Type, Date, Description);

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((Record2)obj);
            }
        }

        sealed class Record1Layout : DelimitedLayout<Record1>
        {
            public Record1Layout()
            {
                HasHeader = false;
                this.WithMember(x => x.Type, c => c.WithName("Type"))
                    .WithMember(x => x.Description, c => c.WithName("Description"))
                    .WithMember(x => x.Value, c => c.WithName("Value"));
            }
        }

        sealed class Record2Layout : DelimitedLayout<Record2>
        {
            public Record2Layout()
            {
                this.WithMember(x => x.Type, c => c.WithName("Type"))
                    .WithMember(x => x.Date, c => c.WithName("Date"))
                    .WithMember(x => x.Description, c => c.WithName("Description"));
            }
        }
    }
}