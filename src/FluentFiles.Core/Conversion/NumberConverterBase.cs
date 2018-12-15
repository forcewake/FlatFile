using System;
using System.Globalization;
using System.Reflection;

namespace FluentFiles.Core.Conversion
{
    /// <summary>
    /// Base class for types that convert between numbers and strings.
    /// It is partially based on <see cref="System.ComponentModel.BaseNumberConverter"/> located at 
    /// https://github.com/dotnet/corefx/blob/master/src/System.ComponentModel.TypeConverter/src/System/ComponentModel/BaseNumberConverter.cs.
    /// </summary>
    public abstract class NumberConverterBase<T> : IFieldValueConverter
    {
        protected Type TargetType { get; } = typeof(T);

        protected abstract T ConvertFromString(ReadOnlySpan<char> source, NumberFormatInfo format);

        protected abstract string ConvertToString(T value, NumberFormatInfo format);

        public bool CanConvert(Type from, Type to) =>
            (from == typeof(string) && to == TargetType) ||
            (from == TargetType && to == typeof(string));

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty)
        {
            var culture = CultureInfo.CurrentCulture;
            var numberFormat = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
            return ConvertFromString(source.Trim(), numberFormat);
        }

        public string ConvertToString(object source, PropertyInfo sourceProperty)
        {
            var culture = CultureInfo.CurrentCulture;
            var numberFormat = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
            return ConvertToString((T)source, numberFormat);
        }
    }
}
