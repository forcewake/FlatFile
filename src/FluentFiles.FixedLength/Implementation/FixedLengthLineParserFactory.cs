using System;
using System.Collections.Generic;
using System.Linq;
using FluentFiles.Core.Base;
using FluentFiles.Core.Extensions;

namespace FluentFiles.FixedLength.Implementation
{
    using Core;

    /// <summary>
    /// Class FixedLengthLineParserFactory.
    /// </summary>
    public class FixedLengthLineParserFactory : IFixedLengthLineParserFactory
    {
        private readonly Dictionary<Type, Type> lineParserRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthLineParserFactory"/> class.
        /// </summary>
        public FixedLengthLineParserFactory()
        {
            lineParserRegistry = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthLineParserFactory"/> class.
        /// </summary>
        /// <param name="lineParserRegistry">The line parser registry.</param>
        public FixedLengthLineParserFactory(IDictionary<Type, Type> lineParserRegistry)
        {
            this.lineParserRegistry = new Dictionary<Type, Type>(lineParserRegistry);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthLineParserFactory"/> class.
        /// </summary>
        /// <param name="lineParserRegistry">The line parser registry.</param>
        public FixedLengthLineParserFactory(IDictionary<Type, IFixedLengthLayoutDescriptor> lineParserRegistry)
        {
            this.lineParserRegistry = lineParserRegistry.ToDictionary(descriptor => descriptor.Key, descriptor => descriptor.Value.TargetType);
        }

        /// <summary>
        /// Gets the parser.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>IFixedLengthLineParser.</returns>
        public IFixedLengthLineParser GetParser(IFixedLengthLayoutDescriptor descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

            return descriptor.TargetType != null && lineParserRegistry.TryGetValue(descriptor.TargetType, out var parserType)
                       ? (IFixedLengthLineParser)ReflectionHelper.CreateInstance(parserType, true, descriptor)
                       : new FixedLengthLineParser(descriptor);
        }

        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetType" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetType">The target record type.</param>
        public void RegisterLineParser<TParser>(Type targetType) where TParser : IFixedLengthLineParser
        {
            lineParserRegistry[targetType] = typeof(TParser);
        }

        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetLayout" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetLayout">The target layout.</param>
        public void RegisterLineParser<TParser>(ILayoutDescriptor<IFieldSettings> targetLayout) where TParser : IFixedLengthLineParser
        {
            lineParserRegistry[targetLayout.TargetType] = typeof(TParser);
        }
    }
}