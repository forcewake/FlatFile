namespace FlatFile.FixedLength.Attributes
{
    using System;
    using FlatFile.Core;
    using FlatFile.FixedLength.Attributes.Infrastructure;

    public static class FlatFileEngineFactoryExtensions
    {
        public static IFlatFileEngine GetEngine<TEntity>(
            this IFlatFileEngineFactory<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer> engineFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            where TEntity : class, new()
        {
            var descriptorProvider = new FixedLayoutDescriptorProvider();

            var descriptor = descriptorProvider.GetDescriptor<TEntity>();

            return engineFactory.GetEngine(descriptor, handleEntryReadError);
        }
    }
}