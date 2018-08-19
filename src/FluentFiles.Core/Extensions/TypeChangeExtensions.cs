namespace FluentFiles.Core.Extensions
{
    using System;
    using System.ComponentModel;

    public static class TypeChangeExtensions
    {
        public static object Convert(this string input, Type targetType)
        {
            var converter = TypeDescriptor.GetConverter(targetType);
            if (converter != null)
            {
                return converter.ConvertFromString(input);
            }
            return targetType.GetDefaultValue();
        }

        public static T Convert<T>(this string input)
        {
            return (T) Convert(input, typeof (T));
        }
    }
}