using System;
using System.Collections.Generic;

namespace FluentFiles.Delimited.Implementation
{
    public class DelimitedLineBuilderFactory : IDelimitedLineBuilderFactory
    {
        private readonly IDictionary<Type, IDelimitedLineBuilder> _lineBuilders = new Dictionary<Type, IDelimitedLineBuilder>();

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