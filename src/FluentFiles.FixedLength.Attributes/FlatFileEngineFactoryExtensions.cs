namespace FluentFiles.FixedLength.Attributes
{
    using FluentFiles.Core;
    using FluentFiles.Core.Base;
    using FluentFiles.FixedLength.Attributes.Infrastructure;
    using FluentFiles.FixedLength.Implementation;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides methods for creating file engines that can handle attribute-based configuration.
    /// </summary>
    public static class FlatFileEngineFactoryExtensions
    {
        /// <summary>
        /// Gets a file engine.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="engineFactory">The engine factory.</param>
        /// <param name="handleEntryReadError">The error handler.</param>
        /// <returns>A new file engine.</returns>
        public static IFlatFileEngine GetEngine<TEntity>(
            this IFlatFileEngineFactory<IFixedLengthLayoutDescriptor, IFixedFieldSettingsContainer> engineFactory,
            FileReadErrorHandler handleEntryReadError = null)
                where TEntity : class, new()
        {
            var descriptorProvider = new FixedLayoutDescriptorProvider();
            var descriptor = descriptorProvider.GetDescriptor<TEntity>();
            return engineFactory.GetEngine(descriptor, handleEntryReadError);
        }

        /// <summary>
        /// Gets a file engine.
        /// </summary>
        /// <param name="engineFactory">The engine factory.</param>
        /// <param name="recordTypes">The record types.</param>
        /// <param name="typeSelectorFunc">The type selector function.</param>
        /// <param name="handleEntryReadError">The error handler.</param>
        /// <returns>A new file engine.</returns>
        public static IFlatFileMultiEngine GetEngine(
            this FixedLengthFileEngineFactory engineFactory,
            IEnumerable<Type> recordTypes,
            Func<string, int, Type> typeSelectorFunc,
            FileReadErrorHandler handleEntryReadError = null)
        {
            var descriptorProvider = new FixedLayoutDescriptorProvider();
            var descriptors = recordTypes.Select(type => descriptorProvider.GetDescriptor(type)).ToList();
            return engineFactory.GetEngine(descriptors, typeSelectorFunc, handleEntryReadError);
        }
    }
}