using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlatFile.Core.Attributes.Extensions
{
    internal static class AttributeUtil
    {
        public static T GetAttribute<T>(this FieldInfo field, bool inherited = true) where T : Attribute
        {
            return field.GetAttributes<T>(inherited).FirstOrDefault();
        }


        public static T GetAttribute<T>(this MemberInfo member, bool inherited = true) where T : Attribute
        {
            return member.GetAttributes<T>(inherited).FirstOrDefault();
        }


        public static T GetAttribute<T>(this PropertyInfo property, bool inherited = true) where T : Attribute
        {
            return property.GetAttributes<T>(inherited).FirstOrDefault();
        }


        public static T GetAttribute<T>(this MethodInfo method, bool inherited = true) where T : Attribute
        {
            return method.GetAttributes<T>(inherited).FirstOrDefault();
        }


        public static T GetAttribute<T>(this ParameterInfo param, bool inherited = true) where T : Attribute
        {
            return param.GetAttributes<T>(inherited).FirstOrDefault();
        }


        public static T GetAttribute<T>(this Type type, bool inherited = true) where T : Attribute
        {
            return type.GetAttributes<T>(inherited).FirstOrDefault();
        }


        public static IEnumerable<T> GetAttributes<T>(this MemberInfo member, bool inherited = true) where T : Attribute
        {
            return (T[]) member.GetCustomAttributes(typeof (T), inherited);
        }


        public static IEnumerable<T> GetAttributes<T>(this PropertyInfo property, bool inherited = true)
            where T : Attribute
        {
            return (T[]) property.GetCustomAttributes(typeof (T), inherited);
        }


        public static IEnumerable<T> GetAttributes<T>(this FieldInfo field, bool inherited = true) where T : Attribute
        {
            return field.GetCustomAttributes(typeof (T), inherited).Cast<T>();
        }


        public static IEnumerable<T> GetAttributes<T>(this MethodInfo method, bool inherited = true) where T : Attribute
        {
            return method.GetCustomAttributes(typeof (T), inherited).Cast<T>();
        }


        public static IEnumerable<T> GetAttributes<T>(this ParameterInfo param, bool inherited = true)
            where T : Attribute
        {
            return param.GetCustomAttributes(typeof (T), inherited).Cast<T>();
        }


        public static IEnumerable<T> GetAttributes<T>(this Type type, bool inherited = true) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttributes(typeof (T), inherited).Cast<T>();
        }
    }
}
