using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FlatFile.Core.Extensions
{
    /// <summary>
    /// Class ReflectionHelper.
    /// </summary>
    public static class ReflectionHelper
    {
        private static readonly ConcurrentDictionary<ConstructorInfo, Func<object>> NoArgCache = new ConcurrentDictionary<ConstructorInfo, Func<object>>();
        private static readonly ConcurrentDictionary<ConstructorInfo, Func<object[], object>> ArgCache = new ConcurrentDictionary<ConstructorInfo, Func<object[], object>>();

        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/> using the default constructor.
        /// </summary>
        public static T CreateInstance<T>(bool cached = false)
        {
            return (T)CreateInstance(typeof(T), cached);
        }

        /// <summary>
        /// Creates an instance of type <paramref name="targetType"/> using the default constructor.
        /// </summary>
        public static object CreateInstance(Type targetType, bool cached = false)
        {
            if (targetType == null) return null;

            var ctorInfo = targetType.GetConstructor(Type.EmptyTypes);
            return CreateInstance(ctorInfo, cached);
        }

        /// <summary>
        /// Creates an instance of type <paramref name="targetType"/> using the specified constructor parameters.
        /// </summary>
        public static object CreateInstance(Type targetType, bool cached = false, params object[] parameters)
        {
            if (targetType == null) return null;
            if (parameters == null || parameters.Length == 0) return CreateInstance(targetType, cached);

            var ctorInfo = targetType.GetConstructor(parameters.Select(a => a.GetType()).ToArray());
            return CreateInstance(ctorInfo, cached, parameters);
        }

        private static object CreateInstance(ConstructorInfo ctorInfo, bool cached, object[] parameters = null)
        {
            if (ctorInfo == null) return null;

            var hasArguments = parameters != null && parameters.Length > 0;
            if (!cached)
            {
                return ctorInfo.Invoke(parameters);
            }

            if (!hasArguments)
            {
                var factory = NoArgCache.GetOrAdd(ctorInfo, BuildNoArgFactory);
                return factory();
            }

            var argFactory = ArgCache.GetOrAdd(ctorInfo, BuildArgFactory);
            return argFactory(parameters);
        }

        private static Func<object> BuildNoArgFactory(ConstructorInfo ctorInfo)
        {
            var ctorCall = Expression.New(ctorInfo);
            var cast = Expression.Convert(ctorCall, typeof(object));
            return Expression.Lambda<Func<object>>(cast).Compile();
        }

        private static Func<object[], object> BuildArgFactory(ConstructorInfo ctorInfo)
        {
            var argsParameter = Expression.Parameter(typeof(object[]), "args");
            var ctorParameters = ctorInfo.GetParameters();

            var arguments = ctorParameters
                .Select((parameter, index) =>
                    Expression.Convert(
                        Expression.ArrayIndex(argsParameter, Expression.Constant(index)),
                        parameter.ParameterType))
                .ToArray();

            var ctorCall = Expression.New(ctorInfo, arguments);
            var cast = Expression.Convert(ctorCall, typeof(object));
            return Expression.Lambda<Func<object[], object>>(cast, argsParameter).Compile();
        }
    }
}
