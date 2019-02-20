namespace FluentFiles.Core.Extensions
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.ComponentModel;

    internal static class TypeExtensions
    {
        public static IFieldValueConverter GetConverter(this Type type)
        {
            if (Configuration.Converters.TryGetValue(type, out var registered))
                return registered;

            var converter = TypeDescriptor.GetConverter(type.Unwrap());
            return converter != null 
                ? new TypeConverterAdapter(converter)
                : DefaultConverter.Instance;
        }

        public static bool IsNullable(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        public static Type Unwrap(this Type type) => type.IsNullable() ? Nullable.GetUnderlyingType(type) : type;

        public static object GetDefaultValue(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsValueType && type != typeof(void))	// Can't create an instance of Void.
                return Activator.CreateInstance(type);

            return null;
        }
    }
}