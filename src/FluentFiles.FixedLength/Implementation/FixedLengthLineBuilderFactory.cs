namespace FluentFiles.FixedLength.Implementation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Creates fixed-length line builders.
    /// </summary>
    public class FixedLengthLineBuilderFactory : IFixedLengthLineBuilderFactory
    {
        private readonly IDictionary<Type, IFixedLengthLineBuilder> _lineBuilders = new Dictionary<Type, IFixedLengthLineBuilder>();

        /// <summary>
        /// Gets a fixed-length line builder.
        /// </summary>
        /// <param name="descriptor">The layout a builder is for.</param>
        /// <returns>A new fixed-length line builder.</returns>
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