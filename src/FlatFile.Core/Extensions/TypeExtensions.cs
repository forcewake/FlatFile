namespace FlatFile.Core.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            // We want an Func<object> which returns the default.
            // Create that expression here.
            Expression<Func<object>> e = Expression.Lambda<Func<object>>(
                // Have to convert to object.
                Expression.Convert(
                    // The default value, always get what the *code* tells us.
                    type.GetDefaultExpression(), typeof(object)
                    )
                );

            // Compile and return the value.
            return e.Compile()();
        }

        public static Expression GetDefaultExpression(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type.GetTypeInfo().IsValueType)
            {
                return Expression.Constant(Activator.CreateInstance(type), type);
            }

            return Expression.Constant(null, type);
        }
    }
}