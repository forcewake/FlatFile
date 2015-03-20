namespace FlatFile.Delimited.Implementation
{
    using System;
    using FlatFile.Core;

    public class DelimitedFileEngineFactory :
        IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
        public IFlatFileEngine<T> GetEngine<T>(
            IDelimitedLayoutDescriptor descriptor,
            Func<string, Exception, bool> handleEntryReadError = null) where T : class, new()
        {
            return new DelimitedFileEngine<T>(
                descriptor,
                new DelimitedLineBuilderFactory(),
                new DelimitedLineParserFactory(),
                handleEntryReadError);
        }
    }
}
