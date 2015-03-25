namespace FlatFile.Delimited.Attributes
{
    using System;
    using FlatFile.Core;
    using FlatFile.Delimited;
    using FlatFile.Delimited.Attributes.Infrastructure;

    public static class FlatFileEngineFactoryExtensions
    {
        public static IFlatFileEngine GetEngine<TEntity>(
            this IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer> engineFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            where TEntity : class, new()
        {
            var descriptorProvider = new DelimitedLayoutDescriptorProvider();

            var descriptor = descriptorProvider.GetDescriptor<TEntity>();

            return engineFactory.GetEngine(descriptor, handleEntryReadError);
        }
    }
}