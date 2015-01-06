namespace FlatFile.Core.Attributes.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FlatFile.Core.Attributes.Entities;

    internal static class TypeExtensions
    {
        public static IEnumerable<PropertyDescription> GetTypeDescription<TAttribute>(this Type targetType) where TAttribute : Attribute
        {
            var properties = from p in targetType.GetProperties()
                where Attribute.IsDefined(p, typeof(TAttribute))
                let attr = p.GetCustomAttributes(typeof(TAttribute), true)
                select new PropertyDescription { Property = p, Attributes = attr.Cast<Attribute>().ToArray() };

            return properties;
        }
    }
}