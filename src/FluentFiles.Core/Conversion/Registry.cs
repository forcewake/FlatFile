using System;
using System.Collections.Generic;

namespace FluentFiles.Core.Conversion
{
    /// <summary>
    /// A global configuration repository. 
    /// </summary>
    public static class Registry
    {
        /// <summary>
        /// The location where the value converter for a type can be specified at a global level.
        /// </summary>
        public static IDictionary<Type, IValueConverter> Converters { get; } = new Dictionary<Type, IValueConverter>();
    }
}
