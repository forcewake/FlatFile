using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlatFile.Core;
using FlatFile.Delimited;
using FlatFile.Delimited.Attributes;
using FlatFile.Delimited.Implementation;
using FluentAssertions;
using Xunit;

namespace FlatFile.Tests.Delimited
{
    public class DelimitedMultiEngineTests
    {
        readonly IFlatFileMultiEngine engine;

        const string TestData = 
@"S,Test Description,00042
D,20150323,Another Description";

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

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Type.GetHashCode();
                    hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ Value;
                    return hashCode;
                }
            }

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

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Type.GetHashCode();
                    hashCode = (hashCode * 397) ^ (Date != null ? Date.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                    return hashCode;
                }
            }

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
                WithMember(x => x.Type, c => c.WithName("Type"))
                    .WithMember(x => x.Description, c => c.WithName("Description"))
                    .WithMember(x => x.Value, c => c.WithName("Value"));
            }
        }

        sealed class Record2Layout : DelimitedLayout<Record2>
        {
            public Record2Layout()
            {
                WithMember(x => x.Type, c => c.WithName("Type"))
                    .WithMember(x => x.Date, c => c.WithName("Date"))
                    .WithMember(x => x.Description, c => c.WithName("Description"));
            }
        }

        public DelimitedMultiEngineTests()
        {

            var factory = new DelimitedFileEngineFactory();
            var types = new System.Collections.Generic.List<Type>()
                {
                    typeof(Record1),
                    typeof(Record2)
                };

            engine = factory.GetEngine(types.AsEnumerable<Type>(),
                                                    s =>
                                                    {
                                                        if (String.IsNullOrEmpty(s) || s.Length < 1) return null;

                                                        switch (s[0])
                                                        {
                                                            case 'S':
                                                                return typeof(Record1);
                                                            case 'D':
                                                                return typeof(Record2);
                                                            default:
                                                                return null;
                                                        }
                                                    }
            );
        }

        [Fact]
        public void EngineShouldReadMultipleRecordTypes()
        {
            using (var stream = GetStringStream(TestData))
                engine.Read(stream);

            var record1Results = engine.GetRecords<Record1>().ToList();
            var record2Results = engine.GetRecords<Record2>().ToList();

            record1Results.Should().HaveCount(1, "because it should read one 'S' record");
            record2Results.Should().HaveCount(1, "because there is one 'D' record");

            record1Results.First().Should().Be(new Record1 { Description = "Test Description", Value = 42 });
            record2Results.First().Should().Be(new Record2 { Description = "Another Description", Date = "20150323" });
        }

        static Stream GetStringStream(string s)
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(s);
            writer.Flush();
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}