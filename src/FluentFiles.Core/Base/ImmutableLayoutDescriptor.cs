namespace FluentFiles.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        /// <summary>
        /// The type a file record maps to.
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        /// The mapping configurations for the fields of a record.
        /// </summary>
        public IEnumerable<TFieldSettings> Fields { get; }

        /// <summary>
        /// Whether or not a record has a header.
        /// </summary>
        public bool HasHeader { get; }

        /// <summary>
        /// Creates instances of <see cref="TargetType"/>.
        /// </summary>
        public Func<object> InstanceFactory { get; }
    }
}
