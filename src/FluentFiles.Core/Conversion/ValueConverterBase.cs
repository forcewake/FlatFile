using System;
using System.Reflection;

namespace FluentFiles.Core.Conversion
{
    /// <summary>
    /// A generic base class for converting between strings and a given type.
    /// </summary>
    /// <typeparam name="TValue">The type to convert to and from a string.</typeparam>
    public abstract class ValueConverterBase<TValue> : IValueConverter
    {
        public virtual bool CanConvert(Type from, Type to) => 
            (from == typeof(string) && to == typeof(TValue)) || 
            (from == typeof(TValue) && to == typeof(string));

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => ConvertFrom(source, targetProperty);

        public ReadOnlySpan<char> ConvertToString(object source, PropertyInfo sourceProperty) => ConvertTo((TValue)source, sourceProperty);

        protected abstract TValue ConvertFrom(ReadOnlySpan<char> source, PropertyInfo targetProperty);

        protected abstract ReadOnlySpan<char> ConvertTo(TValue source, PropertyInfo sourceProperty);
    }
}
