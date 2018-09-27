using FakeItEasy;
using FluentFiles.Core;
using FluentFiles.Core.Base;
using FluentFiles.FixedLength;
using FluentFiles.FixedLength.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentFiles.Tests.FixedLength
{
    public class FixedLengthErrorHandlingTests
    {
        private ILayoutDescriptor<IFixedFieldSettingsContainer> layout;
        readonly IFixedLengthLineParserFactory lineParserFactory;
        readonly IList<FlatFileErrorContext> errorContexts = new List<FlatFileErrorContext>();

        const string TestData = 
@"STest Description    00042
STest Description    00043
STest Description    00044";

        public FixedLengthErrorHandlingTests()
        {
            layout = A.Fake<ILayoutDescriptor<IFixedFieldSettingsContainer>>();
            A.CallTo(() => layout.TargetType).Returns(typeof(Record));
            A.CallTo(() => layout.InstanceFactory).Returns(() => new Record());

            lineParserFactory = new FixedLengthLineParserFactory(new Dictionary<Type, Type>
            {
                { typeof(Record), typeof(FakeLineParser) }
            });
        }

        [Fact]
        public void ErrorContextShouldProvideAccurateInformation()
        {
            var engine = new FixedLengthFileEngine(
                layout,
                A.Fake<IFixedLengthLineBuilderFactory>(),
                lineParserFactory,
                HandleError);

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(TestData)))
                engine.Read<Record>(stream).ToList();

            Assert.Equal(3, errorContexts.Count);
            Assert.Equal(new[] { 1, 2, 3 }, errorContexts.Select(ctx => ctx.LineNumber));
            Assert.Equal(TestData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries), errorContexts.Select(ctx => ctx.Line));
            Assert.All(errorContexts, ctx => Assert.Equal("Parsing failed!", ctx.Exception.Message));
        }

        [Fact]
        public void MultiEngineErrorContextShouldProvideAccurateInformation()
        {
            var engine = new FixedLengthFileMultiEngine(
                new[] { layout },
                (l, i) => typeof(Record),
                A.Fake<IFixedLengthLineBuilderFactory>(),
                lineParserFactory,
                new FixedLengthMasterDetailTracker(),
                HandleError);

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(TestData)))
                engine.Read(stream);

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
            public FakeLineParser(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
            {

            }

            public TEntity ParseLine<TEntity>(in ReadOnlySpan<char> line, TEntity entity) where TEntity : new()
            {
                throw new Exception("Parsing failed!");
            }
        }

        private class Record { }
    }
}
