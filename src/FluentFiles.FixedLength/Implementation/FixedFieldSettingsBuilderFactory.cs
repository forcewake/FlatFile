namespace FluentFiles.FixedLength.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;

    /// <summary>
    /// Creates <see cref="IFixedFieldSettingsBuilder"/>s.
    /// </summary>
    public class FixedFieldSettingsBuilderFactory : IFieldSettingsBuilderFactory<IFixedFieldSettingsBuilder, IFixedFieldSettingsContainer>
    {
        /// <summary>
        /// Gets a builder for a fixed-length field.
        /// </summary>
        /// <typeparam name="TTarget">The type a file record maps to.</typeparam>
        /// <typeparam name="TMember">The type of the member a field within a record maps to.</typeparam>
        /// <param name="member">The member a field within a record maps to.</param>
        /// <returns>A builder for a field.</returns>
        public IFixedFieldSettingsBuilder CreateBuilder<TTarget, TMember>(MemberInfo member) => new FixedFieldSettingsBuilder(member);
    }
}