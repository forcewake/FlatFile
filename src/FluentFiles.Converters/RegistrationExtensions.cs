using FluentFiles.Core.Conversion;
using System;
using System.Collections.Generic;

namespace FluentFiles.Converters
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Registers converters optimized for .NET Core.
        /// </summary>
        public static IDictionary<Type, IValueConverter> UseOptimizedConverters(this IDictionary<Type, IValueConverter> registry)
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
