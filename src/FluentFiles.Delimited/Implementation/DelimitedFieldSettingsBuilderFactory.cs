namespace FluentFiles.Delimited.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;

    /// <summary>
    /// Creates <see cref="IDelimitedFieldSettingsBuilder"/>s.
    /// </summary>
    public class DelimitedFieldSettingsBuilderFactory : IFieldSettingsBuilderFactory<IDelimitedFieldSettingsBuilder, IDelimitedFieldSettingsContainer>
    {
        /// <summary>
        /// Gets a builder for a delimited field.
        /// </summary>
        /// <typeparam name="TTarget">The type a file record maps to.</typeparam>
        /// <typeparam name="TProperty">The type of the property a field within a record maps to.</typeparam>
        /// <param name="property">The property a field within a record maps to.</param>
        /// <returns>A builder for a field.</returns>
        public IDelimitedFieldSettingsBuilder CreateBuilder<TTarget, TProperty>(PropertyInfo property) => new DelimitedFieldSettingsBuilder(property);
    }
}