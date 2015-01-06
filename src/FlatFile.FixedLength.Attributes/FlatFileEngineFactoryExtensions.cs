namespace FlatFile.FixedLength.Attributes
{
    using System;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public static class FlatFileEngineFactoryExtensions
    {
        public static IFlatFileEngine<T> GetEngine<T>(
            this IFlatFileEngineFactory<FixedFieldSettings> engineFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            where T : class, new()
        {
            var container = new FieldsContainer<FixedFieldSettings>();

            var descriptor = new LayoutDescriptorBase<FixedFieldSettings>(container);
            return engineFactory.GetEngine<T>(descriptor, handleEntryReadError);
        }
    }
}