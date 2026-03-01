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

            if (underlyingType == typeof(string))
            {
                return input;
            }

            if (underlyingType == typeof(int))
            {
                return int.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }

            if (underlyingType == typeof(long))
            {
                return long.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }

            if (underlyingType == typeof(short))
            {
                return short.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture);
            }

            if (underlyingType == typeof(decimal))
            {
                return decimal.Parse(input, NumberStyles.Number, CultureInfo.InvariantCulture);
            }

            if (underlyingType == typeof(double))
            {
                return double.Parse(input, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
            }

            if (underlyingType == typeof(float))
            {
                return float.Parse(input, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
            }

            if (underlyingType == typeof(bool))
            {
                return bool.Parse(input);
            }

            if (underlyingType == typeof(Guid))
            {
                return Guid.Parse(input);
            }

            if (underlyingType == typeof(DateTime))
            {
                return DateTime.Parse(input, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
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
