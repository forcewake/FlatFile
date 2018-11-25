using FluentFiles.Core;
using FluentFiles.Core.Base;
using FluentFiles.Delimited.Attributes.Infrastructure;
using FluentFiles.Delimited.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentFiles.Delimited.Attributes
{
    public static class FlatFileEngineFactoryExtensions
    {
        /// <summary>
        /// Gets the engine.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="engineFactory">The engine factory.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        /// <returns>IFlatFileEngine.</returns>
        public static IFlatFileEngine GetEngine<TEntity>(
            this IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer> engineFactory,
            FileReadErrorHandler handleEntryReadError = null)
            where TEntity : class, new()
        {
            var descriptorProvider = new DelimitedLayoutDescriptorProvider();
            var descriptor = descriptorProvider.GetDescriptor<TEntity>();
            return engineFactory.GetEngine(descriptor, handleEntryReadError);
        }

        /// <summary>
        /// Gets the engine.
        /// </summary>
        /// <param name="engineFactory">The engine factory.</param>
        /// <param name="recordTypes">The record types.</param>
        /// <param name="typeSelectorFunc">The type selector function.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        /// <param name="masterDetailTracker">Determines how master-detail record relationships are handled.</param>
        /// <returns>IFlatFileMultiEngine.</returns>
        public static IFlatFileMultiEngine GetEngine(
            this DelimitedFileEngineFactory engineFactory,
            IEnumerable<Type> recordTypes,
            Func<string, Type> typeSelectorFunc,
            FileReadErrorHandler handleEntryReadError = null,
            IMasterDetailStrategy masterDetailTracker = null)
        {
            var descriptorProvider = new DelimitedLayoutDescriptorProvider();
            var descriptors = recordTypes.Select(type => descriptorProvider.GetDescriptor(type)).ToList();
            return engineFactory.GetEngine(descriptors, typeSelectorFunc, handleEntryReadError, masterDetailTracker);
        }
    }
}