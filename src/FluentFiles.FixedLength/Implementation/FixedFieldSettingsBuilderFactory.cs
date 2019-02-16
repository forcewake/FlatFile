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
        /// <typeparam name="TProperty">The type of the property a field within a record maps to.</typeparam>
        /// <param name="property">The property a field within a record maps to.</param>
        /// <returns>A builder for a field.</returns>
        public IFixedFieldSettingsBuilder CreateBuilder<TTarget, TProperty>(PropertyInfo property) => new FixedFieldSettingsBuilder(property);
    }
}