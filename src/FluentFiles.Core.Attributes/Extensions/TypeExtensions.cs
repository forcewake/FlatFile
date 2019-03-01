namespace FluentFiles.Core.Attributes.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using FluentFiles.Core.Attributes.Entities;

    internal static class TypeExtensions
    {
        public static IEnumerable<MemberDescription> GetTypeDescription<TAttribute>(this Type targetType) where TAttribute : Attribute =>
            from member in targetType.GetProperties().Cast<MemberInfo>().Concat(
                           targetType.GetFields())
            where Attribute.IsDefined(member, typeof(TAttribute))
            let attr = member.GetCustomAttributes(typeof(TAttribute), true)
            select new MemberDescription(member, attr.Cast<Attribute>().ToArray());
    }
}