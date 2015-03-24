namespace FlatFile.Delimited.Implementation
{
    using System;
    using FlatFile.Core;

    /// <summary>
    /// Class DelimitedFileEngineFactory.
    /// </summary>
    public class DelimitedFileEngineFactory :
        IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
        /// <summary>
        /// Gets the <see cref="IFlatFileEngine" />.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="handleEntryReadError">The handle entry read error func.</param>
        /// <returns>IFlatFileEngine.</returns>
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
