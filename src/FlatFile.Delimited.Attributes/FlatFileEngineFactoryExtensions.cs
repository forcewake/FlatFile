namespace FlatFile.Delimited.Attributes
{
    using System;
    using FlatFile.Core;
    using FlatFile.Delimited;
    using FlatFile.Delimited.Attributes.Infrastructure;

    public static class FlatFileEngineFactoryExtensions
    {
        public static IFlatFileEngine<T> GetEngine<T>(
            this IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer> engineFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            where T : class, new()
        {
            var descriptorProvider = new DelimitedLayoutDescriptorProvider();

            var descriptor = descriptorProvider.GetDescriptor<T>();

            return engineFactory.GetEngine<T>(descriptor, handleEntryReadError);
        }
    }
}