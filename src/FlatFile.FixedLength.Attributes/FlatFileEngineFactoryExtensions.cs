using System.Collections.Generic;
using System.Linq;
using FlatFile.FixedLength.Implementation;

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

        public static IFlatFileMultiEngine GetEngine(
            this FixedLengthFileEngineFactory engineFactory,
            IEnumerable<Type> recordTypes,
            Func<string, Type> typeSelectorFunc,
            Func<string, Exception, bool> handleEntryReadError = null)
        {
            var descriptorProvider = new FixedLayoutDescriptorProvider();
            var descriptors = recordTypes.Select(type => descriptorProvider.GetDescriptor(type)).ToList();
            return engineFactory.GetEngine(descriptors, typeSelectorFunc, handleEntryReadError);
        }
    }
}