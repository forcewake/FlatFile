namespace FlatFile.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class ReflectionHelper
    {
        private static readonly Dictionary<Type, object> Cache = new Dictionary<Type, object>();

        public static T CreateInstance<T>(bool cached = false)
        {
            return (T)CreateInstance(typeof(T), cached);
        }

        public static object CreateInstance(Type targetType, bool cached = false)
        {
            object value;

            if (!Cache.TryGetValue(targetType, out value) || !cached)
            {
                value = ((Func<object>) Expression.Lambda(Expression.New(targetType), new ParameterExpression[0]).Compile())();
            }

            if (cached && !Cache.ContainsKey(targetType))
            {
                Cache.Add(targetType, value);
            }

            return value;
        }
    }
}