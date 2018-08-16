﻿using System.Collections.Generic;
using System.Linq;
using FlatFile.FixedLength.Implementation;

namespace FlatFile.FixedLength.Attributes
{
    using System;
    using FlatFile.Core;
    using FlatFile.FixedLength.Attributes.Infrastructure;

    /// <summary>
    /// Class FlatFileEngineFactoryExtensions.
    /// </summary>
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
            this IFlatFileEngineFactory<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer> engineFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            where TEntity : class, new()
        {
            var descriptorProvider = new FixedLayoutDescriptorProvider();

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
        /// <returns>IFlatFileMultiEngine.</returns>
        public static IFlatFileMultiEngine GetEngine(
            this FixedLengthFileEngineFactory engineFactory,
            IEnumerable<Type> recordTypes,
            Func<string, int, Type> typeSelectorFunc,
            Func<string, Exception, bool> handleEntryReadError = null)
        {
            var descriptorProvider = new FixedLayoutDescriptorProvider();
            var descriptors = recordTypes.Select(type => descriptorProvider.GetDescriptor(type)).ToList();
            return engineFactory.GetEngine(descriptors, typeSelectorFunc, handleEntryReadError);
        }
    }
}