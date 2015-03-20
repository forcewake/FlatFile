namespace FlatFile.FixedLength.Implementation
{
    using System;
    using FlatFile.Core;

    public class FixedLengthFileEngineFactory : IFlatFileEngineFactory<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>
    {
        public IFlatFileEngine GetEngine(
            ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor,
            Func<string, Exception, bool> handleEntryReadError = null)
        {
            return new FixedLengthFileEngine(
                descriptor, 
                new FixedLengthLineBuilderFactory(),
                new FixedLengthLineParserFactory(), 
                handleEntryReadError);
        }
    }
}