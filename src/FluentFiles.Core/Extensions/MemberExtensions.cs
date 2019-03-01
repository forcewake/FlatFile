namespace FluentFiles.Core.Extensions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Provides extension methods for member-related reflection types.
    /// </summary>
    public static class MemberExtensions
    {
        /// <summary>
        /// Returns the member type if it is a <see cref="PropertyInfo"/> or <see cref="FieldInfo"/>.
        /// Otherwise, an exception is thrown.
        /// </summary>
        /// <param name="member">The member whose type is being retrieved.</param>
        /// <returns>The member's type.</returns>
        /// <exception cref="InvalidOperationException">If the member is not a property or field.</exception>
        public static Type MemberType(this MemberInfo member)
        {
            if (member is PropertyInfo property)
                return property.PropertyType;

            if (member is FieldInfo field)
                return field.FieldType;

            throw new InvalidOperationException($"Invalid member: {member.DeclaringType.Name}.{member.Name}");
        }
    }
}
