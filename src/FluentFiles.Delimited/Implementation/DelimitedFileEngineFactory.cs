﻿namespace FluentFiles.Delimited.Implementation
{
    using FluentFiles.Core;
    using FluentFiles.Core.Base;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class DelimitedFileEngineFactory.
    /// </summary>
    public class DelimitedFileEngineFactory :
        IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {

        readonly DelimitedLineParserFactory lineParserFactory = new DelimitedLineParserFactory();

        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetType" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetType">The target record type.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RegisterLineParser<TParser>(Type targetType) where TParser : IDelimitedLineParser
        {
            lineParserFactory.RegisterLineParser<TParser>(targetType);
        }

        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetLayout" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetLayout">The target layout.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RegisterLineParser<TParser>(ILayoutDescriptor<IFieldSettings> targetLayout) where TParser : IDelimitedLineParser
        {
            lineParserFactory.RegisterLineParser<TParser>(targetLayout);
        }

        /// <summary>
        /// Gets the <see cref="IFlatFileEngine" />.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="handleEntryReadError">The handle entry read error func.</param>
        /// <returns>IFlatFileEngine.</returns>
        public IFlatFileEngine GetEngine(
            IDelimitedLayoutDescriptor descriptor,
            FileReadErrorHandler handleEntryReadError = null)
        {
            return new DelimitedFileEngine(
                descriptor,
                new DelimitedLineBuilderFactory(),
                new DelimitedLineParserFactory(),
                handleEntryReadError);
        }

        /// <summary>
        /// Gets the <see cref="IFlatFileMultiEngine"/>.
        /// </summary>
        /// <param name="layoutDescriptors">The layout descriptors.</param>
        /// <param name="typeSelectorFunc">The type selector function.</param>
        /// <param name="handleEntryReadError">The handle entry read error func.</param>
        /// <param name="masterDetailStrategy">Determines how master-detail record relationships are handled.</param>
        /// <returns>IFlatFileMultiEngine.</returns>
        public IFlatFileMultiEngine GetEngine(
            IEnumerable<IDelimitedLayoutDescriptor> layoutDescriptors,
            Func<string, int, Type> typeSelectorFunc,
            FileReadErrorHandler handleEntryReadError = null,
            IMasterDetailStrategy masterDetailStrategy = null)
        {
            return new DelimitedFileMultiEngine(
                layoutDescriptors,
                typeSelectorFunc,
                new DelimitedLineBuilderFactory(),
                lineParserFactory,
                masterDetailStrategy ?? new DefaultDelimitedMasterDetailStrategy(),
                handleEntryReadError);
        }
    }
}
