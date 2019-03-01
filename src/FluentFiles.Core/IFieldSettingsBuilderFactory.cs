namespace FluentFiles.Core
{
    using System.Reflection;

    /// <summary>
    /// Interface for objects that create <see cref="IFieldSettingsBuilder{TBuilder, TSettings}"/>s. 
    /// </summary>
    /// <typeparam name="TBuilder">The type of field builder to create.</typeparam>
    /// <typeparam name="TSettings">The type of field configuration a builder configures and produces.</typeparam>
    public interface IFieldSettingsBuilderFactory<out TBuilder, out TSettings>
        where TBuilder : IFieldSettingsBuilder<TBuilder, TSettings>, IBuildable<TSettings>
        where TSettings : IFieldSettings
    {
        /// <summary>
        /// Creates a field builder.
        /// </summary>
        /// <typeparam name="TTarget">The type a field's target property is a member of.</typeparam>
        /// <typeparam name="TMember">The type of member a field maps to.</typeparam>
        /// <param name="member">The member a field maps to.</param>
        /// <returns>A new field configuration builder.</returns>
        TBuilder CreateBuilder<TTarget, TMember>(MemberInfo member);
    }
}