namespace FluentFiles.Core
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a file record layout.
    /// </summary>
    /// <typeparam name="TTarget">The type a file record maps to.</typeparam>
    /// <typeparam name="TFieldSettings">The type of individual field mapping within a layout.</typeparam>
    /// <typeparam name="TBuilder">The field builder type.</typeparam>
    /// <typeparam name="TLayout">The self-referencing type of layout.</typeparam>
    public interface ILayout<TTarget, TFieldSettings, TBuilder, out TLayout> : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
        where TBuilder : IFieldSettingsBuilder<TBuilder, TFieldSettings>
        where TLayout : ILayout<TTarget, TFieldSettings, TBuilder, TLayout>
    {
        /// <summary>
        /// Configures a mapping from a record field to a member of a type.
        /// </summary>
        /// <typeparam name="TProperty">The type of the member a field maps to.</typeparam>
        /// <param name="expression">An expression selecting the member to map to.</param>
        /// <param name="configure">An action that performs configuration of a field mapping.</param>
        TLayout WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TBuilder> configure = null);

        /// <summary>
        /// Indicates that a record layout contains a header.
        /// </summary>
        TLayout WithHeader();
    }
}