namespace FluentFiles.Core.Attributes.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentFiles.Core.Attributes.Entities;

    internal static class TypeExtensions
    {
        public static IEnumerable<PropertyDescription> GetTypeDescription<TAttribute>(this Type targetType) where TAttribute : Attribute =>
            from property in targetType.GetProperties()
            where Attribute.IsDefined(property, typeof(TAttribute))
            let attr = property.GetCustomAttributes(typeof(TAttribute), true)
            select new PropertyDescription(property, attr.Cast<Attribute>().ToArray());
    }
}