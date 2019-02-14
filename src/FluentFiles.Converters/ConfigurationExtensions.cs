namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides methods for customizing field value conversion.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Registers converters optimized for .NET Core.
        /// </summary>
        public static IDictionary<Type, IFieldValueConverter> UseOptimizedConverters(this IDictionary<Type, IFieldValueConverter> registry)
        {
            registry[typeof(char)] = new CharConverter();
            registry[typeof(byte)] = new ByteConverter();
            registry[typeof(short)] = new Int16Converter();
            registry[typeof(int)] = new Int32Converter();
            registry[typeof(long)] = new Int64Converter();
            registry[typeof(float)] = new SingleConverter();
            registry[typeof(double)] = new DoubleConverter();
            registry[typeof(decimal)] = new DecimalConverter();
            registry[typeof(Guid)] = new GuidConverter();
            registry[typeof(string)] = new StringConverter();
            return registry;
        }
    }
}
