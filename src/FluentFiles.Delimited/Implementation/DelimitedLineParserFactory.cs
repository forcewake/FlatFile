using System;
using System.Collections.Generic;
using System.Linq;
using FluentFiles.Core;
using FluentFiles.Core.Base;
using FluentFiles.Core.Extensions;

namespace FluentFiles.Delimited.Implementation
{
    public class DelimitedLineParserFactory : IDelimitedLineParserFactory
    {
        readonly Dictionary<Type, Type> lineParserRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelimitedLineParserFactory"/> class.
        /// </summary>
        public DelimitedLineParserFactory() { lineParserRegistry = new Dictionary<Type, Type>(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelimitedLineParserFactory"/> class.
        /// </summary>
        /// <param name="lineParserRegistry">The line parser registry.</param>
        public DelimitedLineParserFactory(IDictionary<Type, Type> lineParserRegistry) { this.lineParserRegistry = new Dictionary<Type, Type>(lineParserRegistry); }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelimitedLineParserFactory"/> class.
        /// </summary>
        /// <param name="lineParserRegistry">The line parser registry.</param>
        public DelimitedLineParserFactory(IDictionary<Type, ILayoutDescriptor<IDelimitedFieldSettingsContainer>> lineParserRegistry)
        {
            this.lineParserRegistry = lineParserRegistry.ToDictionary(descriptor => descriptor.Key, descriptor => descriptor.Value.TargetType);
        }

        public IDelimitedLineParser GetParser(IDelimitedLayoutDescriptor descriptor)
        {
            return new DelimitedLineParser(descriptor);
        }

        /// <summary>
        /// Gets the parser.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>IFixedLengthLineParser.</returns>
        public IDelimitedLineParser GetParser(ILayoutDescriptor<IDelimitedFieldSettingsContainer> descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor");

            if (!(descriptor is IDelimitedLayoutDescriptor)) throw new ArgumentException("descriptor must be IDelimitedLayoutDescriptor");
            
            return descriptor.TargetType != null && lineParserRegistry.ContainsKey(descriptor.TargetType)
                       ? (IDelimitedLineParser)ReflectionHelper.CreateInstance(lineParserRegistry[descriptor.TargetType], true, descriptor)
                       : new DelimitedLineParser((IDelimitedLayoutDescriptor)descriptor);
        }
        
        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetType" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetType">The target record type.</param>
        public void RegisterLineParser<TParser>(Type targetType) where TParser : IDelimitedLineParser
        {
            lineParserRegistry[targetType] = typeof(TParser);
        }

        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetLayout" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetLayout">The target layout.</param>
        public void RegisterLineParser<TParser>(ILayoutDescriptor<IFieldSettings> targetLayout) where TParser : IDelimitedLineParser
        {
            lineParserRegistry[targetLayout.TargetType] = typeof(TParser);
        }

    }
}