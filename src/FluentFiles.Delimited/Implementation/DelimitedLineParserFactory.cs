namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentFiles.Core;
    using FluentFiles.Core.Extensions;

    /// <summary>
    /// Creates parsers for delimited files.
    /// </summary>
    public class DelimitedLineParserFactory : IDelimitedLineParserFactory
    {
        private readonly Dictionary<Type, Type> _parserRegistry;
        private readonly IDictionary<Type, IDelimitedLineParser> _parsers = new Dictionary<Type, IDelimitedLineParser>();

        /// <summary>
        /// Initializes a new <see cref="DelimitedLineParserFactory"/>.
        /// </summary>
        /// <param name="parserRegistry">The line parser registry.</param>
        private DelimitedLineParserFactory(Dictionary<Type, Type> parserRegistry)
        {
            _parserRegistry = parserRegistry;
        }

        /// <summary>
        /// Initializes a new <see cref="DelimitedLineParserFactory"/>.
        /// </summary>
        public DelimitedLineParserFactory()
            : this(new Dictionary<Type, Type>())
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DelimitedLineParserFactory"/>.
        /// </summary>
        /// <param name="lineParserRegistry">The line parser registry.</param>
        public DelimitedLineParserFactory(IDictionary<Type, Type> lineParserRegistry)
            : this(new Dictionary<Type, Type>(lineParserRegistry))
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DelimitedLineParserFactory"/>.
        /// </summary>
        /// <param name="lineParserRegistry">The line parser registry.</param>
        public DelimitedLineParserFactory(IDictionary<Type, ILayoutDescriptor<IDelimitedFieldSettingsContainer>> lineParserRegistry)
            : this(lineParserRegistry.ToDictionary(descriptor => descriptor.Key, descriptor => descriptor.Value.TargetType))
        {
        }

        /// <summary>
        /// Gets the parser.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>IFixedLengthLineParser.</returns>
        public IDelimitedLineParser GetParser(IDelimitedLayoutDescriptor descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));

            if (!_parsers.TryGetValue(descriptor.TargetType, out var parser))
            {
                parser = descriptor.TargetType != null && _parserRegistry.ContainsKey(descriptor.TargetType)
                    ? (IDelimitedLineParser)ReflectionHelper.CreateInstance(_parserRegistry[descriptor.TargetType], true, descriptor)
                    : new DelimitedLineParser(descriptor);

                _parsers[descriptor.TargetType] = parser;
            }

            return parser;
        }
        
        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetType" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetType">The target record type.</param>
        public void RegisterLineParser<TParser>(Type targetType) where TParser : IDelimitedLineParser
        {
            _parserRegistry[targetType] = typeof(TParser);
        }

        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetLayout" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetLayout">The target layout.</param>
        public void RegisterLineParser<TParser>(ILayoutDescriptor<IFieldSettings> targetLayout) where TParser : IDelimitedLineParser
        {
            _parserRegistry[targetLayout.TargetType] = typeof(TParser);
        }

    }
}