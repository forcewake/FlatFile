using System.Collections.Generic;

namespace FlatFile.FixedLength.Implementation
{
    using System;
    using FlatFile.Core;

    /// <summary>
    /// Class FixedLengthFileEngineFactory.
    /// </summary>
    public class FixedLengthFileEngineFactory : IFlatFileEngineFactory<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>
    {
        /// <summary>
        /// Gets the <see cref="IFlatFileEngine" />.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="handleEntryReadError">The handle entry read error func.</param>
        /// <returns>IFlatFileEngine.</returns>
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

        /// <summary>
        /// Gets the <see cref="IFlatFileMultiEngine"/>.
        /// </summary>
        /// <param name="layoutDescriptors">The layout descriptors.</param>
        /// <param name="typeSelectorFunc">The type selector function.</param>
        /// <param name="handleEntryReadError">The handle entry read error func.</param>
        /// <returns>IFlatFileMultiEngine.</returns>
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