namespace FlatFile.FixedLength.Attributes
{
    using System;
    using FlatFile.Core;
    using FlatFile.FixedLength.Attributes.Infrastructure;

    public static class FlatFileEngineFactoryExtensions
    {
        public static IFlatFileEngine<T> GetEngine<T>(
            this IFlatFileEngineFactory<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer> engineFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            where T : class, new()
        {
            var descriptorProvider = new FixedLayoutDescriptorProvider();

            var descriptor = descriptorProvider.GetDescriptor<T>();

            return engineFactory.GetEngine<T>(descriptor, handleEntryReadError);
        }
    }
}