using System.Collections.Generic;

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

        public IFlatFileMultiEngine GetEngine(
            IEnumerable<ILayoutDescriptor<IFixedFieldSettingsContainer>> layoutDescriptors,
            Func<string, Type> typeSelectorFunc,
            Func<string, Exception, bool> handleEntryReadError = null)
        {
            return new FixedLengthFileMultiEngine(
                layoutDescriptors,
                typeSelectorFunc,
                new FixedLengthLineBuilderFactory(),
                new FixedLengthLineParserFactory(),
                handleEntryReadError);
        }
    }
}