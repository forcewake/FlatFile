namespace FlatFile.Core.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class PropertyAccessorCache
    {
        private static readonly ConcurrentDictionary<PropertyInfo, Func<object, object>> GetterCache =
            new ConcurrentDictionary<PropertyInfo, Func<object, object>>();

        private static readonly ConcurrentDictionary<PropertyInfo, Action<object, object>> SetterCache =
            new ConcurrentDictionary<PropertyInfo, Action<object, object>>();

        public static object GetValue(PropertyInfo propertyInfo, object target)
        {
            var getter = GetterCache.GetOrAdd(propertyInfo, BuildGetter);
            return getter(target);
        }

        public static void SetValue(PropertyInfo propertyInfo, object target, object value)
        {
            var setter = SetterCache.GetOrAdd(propertyInfo, BuildSetter);
            setter(target, value);
        }

        private static Func<object, object> BuildGetter(PropertyInfo propertyInfo)
        {
            var target = Expression.Parameter(typeof(object), "target");
            var castTarget = Expression.Convert(target, propertyInfo.DeclaringType);
            var property = Expression.Property(castTarget, propertyInfo);
            var castResult = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<object, object>>(castResult, target).Compile();
        }

        private static Action<object, object> BuildSetter(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
            {
                return (target, value) => { };
            }

            var target = Expression.Parameter(typeof(object), "target");
            var value = Expression.Parameter(typeof(object), "value");
            var castTarget = Expression.Convert(target, propertyInfo.DeclaringType);
            var castValue = Expression.Convert(value, propertyInfo.PropertyType);
            var property = Expression.Property(castTarget, propertyInfo);
            var assign = Expression.Assign(property, castValue);
            return Expression.Lambda<Action<object, object>>(assign, target, value).Compile();
        }
    }
}
