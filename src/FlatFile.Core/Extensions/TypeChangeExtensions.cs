namespace FlatFile.Core.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Globalization;

    public static class TypeChangeExtensions
    {
        private static readonly ConcurrentDictionary<Type, TypeConverter> ConverterCache = new ConcurrentDictionary<Type, TypeConverter>();

        public static object Convert(this string input, Type targetType)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException("targetType");
            }

            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            if (string.IsNullOrWhiteSpace(input))
            {
                return Nullable.GetUnderlyingType(targetType) != null
                    ? null
                    : targetType.GetDefaultValue();
            }

            if (underlyingType.IsEnum)
            {
                return Enum.Parse(underlyingType, input, true);
            }

            var converter = ConverterCache.GetOrAdd(underlyingType, TypeDescriptor.GetConverter);
            if (converter != null)
            {
                if (converter.CanConvertFrom(typeof(string)))
                {
                    return converter.ConvertFromInvariantString(input);
                }

                if (converter.CanConvertFrom(typeof(object)))
                {
                    return converter.ConvertFrom(null, CultureInfo.InvariantCulture, input);
                }
            }

            return System.Convert.ChangeType(input, underlyingType, CultureInfo.InvariantCulture);
        }

        public static T Convert<T>(this string input)
        {
            return (T)Convert(input, typeof(T));
        }
    }
}
