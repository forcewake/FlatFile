using System;
using System.Collections.Generic;

namespace FluentFiles.FixedLength.Implementation
{
    public class FixedLengthLineBuilderFactory : IFixedLengthLineBuilderFactory
    {
        private readonly IDictionary<Type, IFixedLengthLineBuilder> _lineBuilders = new Dictionary<Type, IFixedLengthLineBuilder>();

        public IFixedLengthLineBuilder GetBuilder(IFixedLengthLayoutDescriptor descriptor)
        {
            if (!_lineBuilders.TryGetValue(descriptor.TargetType, out var builder))
            {
                builder = new FixedLengthLineBuilder(descriptor);
                _lineBuilders[descriptor.TargetType] = builder;
            }

            return builder;
        }
    }
}