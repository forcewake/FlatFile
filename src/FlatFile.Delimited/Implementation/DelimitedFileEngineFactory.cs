namespace FlatFile.Delimited.Implementation
{
    using System;
    using FlatFile.Core;

    public class DelimitedFileEngineFactory :
        IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
        public IFlatFileEngine GetEngine(
            IDelimitedLayoutDescriptor descriptor,
            Func<string, Exception, bool> handleEntryReadError = null)
        {
            return new DelimitedFileEngine(
                descriptor,
                new DelimitedLineBuilderFactory(),
                new DelimitedLineParserFactory(),
                handleEntryReadError);
        }
    }
}
