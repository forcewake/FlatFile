using FakeItEasy;
using FluentFiles.Core.Base;
using FluentFiles.Delimited;
using FluentFiles.Delimited.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentFiles.Tests.Delimited
{
    public class DelimitedErrorHandlingTests
    {
        private readonly IDelimitedLayoutDescriptor layout;
        private readonly IDelimitedLineParserFactory lineParserFactory;
        private readonly IList<FlatFileErrorContext> errorContexts = new List<FlatFileErrorContext>();

        const string TestData = 
@"S,Test Description,00042
S,Test Description,00043
S,Test Description,00044";

        public DelimitedErrorHandlingTests()
        {
            layout = A.Fake<IDelimitedLayoutDescriptor>();
            A.CallTo(() => layout.TargetType).Returns(typeof(Record));
            A.CallTo(() => layout.InstanceFactory).Returns(() => new Record());

            lineParserFactory = A.Fake<IDelimitedLineParserFactory>();
            A.CallTo(() => lineParserFactory.GetParser(A<IDelimitedLayoutDescriptor>.Ignored))
                .Returns(new FakeLineParser());
        }

        [Fact]
        public void ErrorContextShouldProvideAccurateInformation()
        {
            var engine = new DelimitedFileEngine(
                layout,
                A.Fake<IDelimitedLineBuilderFactory>(),
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
            var engine = new DelimitedFileMultiEngine(
                new[] { layout },
                (l, n) => typeof(Record),
                A.Fake<IDelimitedLineBuilderFactory>(),
                lineParserFactory,
                new DefaultDelimitedMasterDetailStrategy(),
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

        private class FakeLineParser : IDelimitedLineParser
        {
            public TEntity ParseLine<TEntity>(ReadOnlySpan<char> line, TEntity entity) where TEntity : new() => 
                throw new Exception("Parsing failed!");
        }

        private class Record { }
    }
}
