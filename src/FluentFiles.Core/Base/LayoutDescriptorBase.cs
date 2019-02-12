namespace FluentFiles.Core.Base
{
    using System;
    using System.Collections.Generic;
    using FluentFiles.Core.Extensions;

    /// <summary>
    /// Base class for layout descriptors.
    /// </summary>
    /// <typeparam name="TFieldSettings"></typeparam>
    public class LayoutDescriptorBase<TFieldSettings> : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        /// <summary>
        /// The mapping configurations for the fields of a record.
        /// </summary>
        protected IFieldCollection<TFieldSettings> FieldCollection { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="LayoutDescriptorBase{TFieldSettings}"/>.
        /// </summary>
        /// <param name="fieldCollection">The mapping configurations for the fields of a record.</param>
        protected LayoutDescriptorBase(IFieldCollection<TFieldSettings> fieldCollection)
        {
            FieldCollection = fieldCollection;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LayoutDescriptorBase{TFieldSettings}"/>.
        /// </summary>
        /// <param name="fieldCollection">The mapping configurations for the fields of a record.</param>
        /// <param name="targetType">The type a file record maps to.</param>
        public LayoutDescriptorBase(IFieldCollection<TFieldSettings> fieldCollection, Type targetType)
            : this(fieldCollection)
        {
            TargetType = targetType;
            InstanceFactory = ReflectionHelper.CreateConstructor(targetType);
        }

        /// <summary>
        /// The type a file record maps to.
        /// </summary>
        public virtual Type TargetType { get; }

        /// <summary>
        /// The mapping configurations for the fields of a record.
        /// </summary>
        public IEnumerable<TFieldSettings> Fields => FieldCollection;

        /// <summary>
        /// Whether or not a record has a header.
        /// </summary>
        public bool HasHeader { get; protected internal set; }

        /// <summary>
        /// Creates instances of <see cref="TargetType"/>.
        /// </summary>
        public virtual Func<object> InstanceFactory { get; }
    }
}