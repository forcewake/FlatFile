using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentFiles.Core.Base
{
    /// <summary>
    /// A layout descriptor that has been finalized and cannot be changed.
    /// </summary>
    public abstract class ImmutableLayoutDescriptor<TFieldSettings> : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ImmutableLayoutDescriptor{TFieldSettings}"/> by copying another
        /// <see cref="ILayoutDescriptor{TFieldSettings}"/>.
        /// </summary>
        /// <param name="existing">The descriptor to copy.</param>
        protected ImmutableLayoutDescriptor(ILayoutDescriptor<TFieldSettings> existing)
        {
            TargetType = existing.TargetType;
            Fields = existing.Fields.ToList();
            HasHeader = existing.HasHeader;
            InstanceFactory = existing.InstanceFactory;
        }

        public Type TargetType { get; }

        public IEnumerable<TFieldSettings> Fields { get; }

        public bool HasHeader { get; }

        public Func<object> InstanceFactory { get; }
    }
}
