namespace FlatFile.Core.Attributes.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using FlatFile.Core.Attributes.Entities;

    internal static class TypeExtensions
    {
        public static IEnumerable<PropertyDescription> GetTypeDescription<TAttribute>(this Type targetType) where TAttribute : Attribute
        {
            var properties = from p in targetType.GetRuntimeProperties()
                where p.GetAttribute<TAttribute>() != null
                let attr = p.GetCustomAttributes(typeof(TAttribute), true)
                select new PropertyDescription { Property = p, Attributes = attr.ToArray() };

            return properties;
        }
    }
}
