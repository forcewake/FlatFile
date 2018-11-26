using FluentFiles.Core.Conversion;
using System;
using System.Collections.Generic;

namespace FluentFiles.Core
{
    /// <summary>
    /// A global configuration repository. 
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The location where the value converter for a type can be specified at a global level.
        /// </summary>
        public static IDictionary<Type, IFieldValueConverter> Converters { get; } = new Dictionary<Type, IFieldValueConverter>();
    }
}
