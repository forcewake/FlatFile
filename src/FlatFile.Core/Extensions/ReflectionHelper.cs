namespace FlatFile.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Class ReflectionHelper.
    /// </summary>
    public static class ReflectionHelper
    {
        static readonly object CacheLock = new object();
        static readonly Dictionary<ConstructorInfo, Delegate> Cache = new Dictionary<ConstructorInfo, Delegate>();

        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/> using the default constructor.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cached">if set to <c>true</c> [cached].</param>
        /// <returns>T.</returns>
        public static T CreateInstance<T>(bool cached = false) { return (T) CreateInstance(typeof (T), cached); }

        /// <summary>
        /// Creates an instance of type <paramref name="targetType"/> using the default constructor.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="cached">if set to <c>true</c> [cached].</param>
        /// <returns>System.Object.</returns>
        public static object CreateInstance(Type targetType, bool cached = false)
        {
            if (targetType == null) return null;

            var ctorInfo = targetType.GetTypeInfo().GetConstructor(Type.EmptyTypes);

            return CreateInstance(ctorInfo, cached);
        }

        /// <summary>
        /// Creates an instance of type <paramref name="targetType"/> using the specified constructor parameters.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="cached">if set to <c>true</c> [cached].</param>
        /// <param name="parameters">The constructor arguments.</param>
        /// <returns>System.Object.</returns>
        public static object CreateInstance(Type targetType, bool cached = false, params object[] parameters)
        {
            if (targetType == null) return null;
            if (parameters == null || !parameters.Any()) return CreateInstance(targetType, cached);

            var ctorInfo = targetType.GetTypeInfo().GetConstructor(parameters.Select(a => a.GetType()).ToArray());

            return CreateInstance(ctorInfo, cached, parameters);
        }

        static object CreateInstance(ConstructorInfo ctorInfo, bool cached, object[] parameters = null)
        {
            if (ctorInfo == null) return null;
            var hasArguments = parameters != null && parameters.Any();

            Delegate ctor;
            lock (CacheLock)
            {
                if (!Cache.TryGetValue(ctorInfo, out ctor) || !cached)
                {
                    if (hasArguments)
                    {
                        var ctorArgs = ctorInfo.GetParameters().Select((param, index) => Expression.Parameter(param.ParameterType, String.Format("Param{0}", index))).ToArray();
                        // ReSharper disable once CoVariantArrayConversion
                        ctor = Expression.Lambda(Expression.New(ctorInfo, ctorArgs), ctorArgs).Compile();
                    }
                    else
                    {
                        ctor = Expression.Lambda(Expression.New(ctorInfo)).Compile();
                    }
                }

                if (cached) CacheCtor(ctorInfo, ctor);
            }
            return hasArguments ? ctor.DynamicInvoke(parameters) : ctor.DynamicInvoke();
        }

        static void CacheCtor(ConstructorInfo key, Delegate ctor) { if (!Cache.ContainsKey(key)) Cache.Add(key, ctor); }
    }
}