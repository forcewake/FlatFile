namespace FlatFile.FixedLength.Implementation
{
    using System;
    using FlatFile.Core;

    public class FixedLengthFileEngineFactory : IFlatFileEngineFactory<FixedFieldSettings>
    {
        public IFlatFileEngine<T> GetEngine<T>(
            ILayoutDescriptor<FixedFieldSettings> descriptor,
            Func<string, Exception, bool> handleEntryReadError = null)
            where T : class, new()
        {
            return new FixedLengthFileEngine<T>(
                descriptor, 
                new FixedLengthLineBuilderFactory<T>(),
                new FixedLengthLineParserFactory<T>(), 
                handleEntryReadError);
        }
    }
}