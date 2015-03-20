namespace FlatFile.FixedLength.Implementation
{
    using System;
    using FlatFile.Core;

    public class FixedLengthFileEngineFactory : IFlatFileEngineFactory<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>
    {
        public IFlatFileEngine<T> GetEngine<T>(
            ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor,
            Func<string, Exception, bool> handleEntryReadError = null)
            where T : class, new()
        {
            return new FixedLengthFileEngine<T>(
                descriptor, 
                new FixedLengthLineBuilderFactory(),
                new FixedLengthLineParserFactory(), 
                handleEntryReadError);
        }
    }
}