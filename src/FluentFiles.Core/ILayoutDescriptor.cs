namespace FluentFiles.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Describes the mapping from a file record to a target type.
    /// </summary>
    /// <typeparam name="TFieldSettings"></typeparam>
    public interface ILayoutDescriptor<TFieldSettings> where TFieldSettings : IFieldSettings
    {
        /// <summary>
        /// The type a file record maps to.
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// The mapping configurations for the fields of a record.
        /// </summary>
        IEnumerable<TFieldSettings> Fields { get; }

        /// <summary>
        /// Whether or not a record has a header.
        /// </summary>
        bool HasHeader { get; }

        /// <summary>
        /// Creates instances of <see cref="TargetType"/>.
        /// </summary>
        Func<object> InstanceFactory { get; }
    }
}