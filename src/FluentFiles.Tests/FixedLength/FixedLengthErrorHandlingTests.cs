using FakeItEasy;
using FluentFiles.Core.Base;
using FluentFiles.FixedLength;
using FluentFiles.FixedLength.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentFiles.Tests.FixedLength
{
    public class FixedLengthErrorHandlingTests
    {
        private readonly IFixedLengthLayoutDescriptor layout;
        private readonly IFixedLengthLineParserFactory lineParserFactory;
        private readonly IList<FlatFileErrorContext> errorContexts = new List<FlatFileErrorContext>();

        const string TestData = 
@"STest Description    00042
STest Description    00043
STest Description    00044";

        public FixedLengthErrorHandlingTests()
        {
            layout = A.Fake<IFixedLengthLayoutDescriptor>();
            A.CallTo(() => layout.TargetType).Returns(typeof(Record));
            A.CallTo(() => layout.InstanceFactory).Returns(() => new Record());

            lineParserFactory = A.Fake<IFixedLengthLineParserFactory>();
            A.CallTo(() => lineParserFactory.GetParser(A<IFixedLengthLayoutDescriptor>.Ignored))
                .Returns(new FakeLineParser());
        }

        [Fact]
        public void ErrorContextShouldProvideAccurateInformation()
        {
            var engine = new FixedLengthFileEngine(
                layout,
                A.Fake<IFixedLengthLineBuilderFactory>(),
                lineParserFactory,
                HandleError);

            using (var reader = new StringReader(TestData))
                engine.Read<Record>(reader).ToList();

            Assert.Equal(3, errorContexts.Count);
            Assert.Equal(new[] { 1, 2, 3 }, errorContexts.Select(ctx => ctx.LineNumber));
            Assert.Equal(TestData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries), errorContexts.Select(ctx => ctx.Line));
            Assert.All(errorContexts, ctx => Assert.Equal("Parsing failed!", ctx.Exception.Message));
        }

        [Fact]
        public async Task MultiEngineErrorContextShouldProvideAccurateInformation()
        {
            var engine = new FixedLengthFileMultiEngine(
                new[] { layout },
                (l, i) => typeof(Record),
                A.Fake<IFixedLengthLineBuilderFactory>(),
                lineParserFactory,
                new DefaultFixedLengthMasterDetailStrategy(),
                HandleError);

            using (var reader = new StringReader(TestData))
                await engine.ReadAsync(reader);

            Assert.Equal(3, errorContexts.Count);
            Assert.Equal(new[] { 1, 2, 3 }, errorContexts.Select(ctx => ctx.LineNumber));
            Assert.Equal(TestData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries), errorContexts.Select(ctx => ctx.Line));
            Assert.All(errorContexts, ctx => Assert.Equal("Parsing failed!", ctx.Exception.Message));
        }

        private bool HandleError(FlatFileErrorContext context)
        {
            errorContexts.Add(context);
            return true;
        }

        private class FakeLineParser : IFixedLengthLineParser
        {
            public TEntity ParseLine<TEntity>(ReadOnlySpan<char> line, TEntity entity) where TEntity : new() =>
                throw new Exception("Parsing failed!");
        }

        private class Record { }
    }
}
