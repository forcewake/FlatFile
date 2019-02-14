using System;
using System.Collections.Generic;

namespace FluentFiles.Delimited.Implementation
{
    /// <summary>
    /// Creates delimited line builders.
    /// </summary>
    public class DelimitedLineBuilderFactory : IDelimitedLineBuilderFactory
    {
        private readonly IDictionary<Type, IDelimitedLineBuilder> _lineBuilders = new Dictionary<Type, IDelimitedLineBuilder>();

        /// <summary>
        /// Gets a delimited line builder.
        /// </summary>
        /// <param name="descriptor">The layout a builder is for.</param>
        /// <returns>A new delimited line builder.</returns>
        public IDelimitedLineBuilder GetBuilder(IDelimitedLayoutDescriptor descriptor)
        {
            if (!_lineBuilders.TryGetValue(descriptor.TargetType, out var builder))
            {
                builder = new DelimitedLineBuilder(descriptor);
                _lineBuilders[descriptor.TargetType] = builder;
            }

            return builder;
        }
    }
}