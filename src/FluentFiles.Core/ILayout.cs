namespace FluentFiles.Core
{
    using System;
    using System.Linq.Expressions;
    using FluentFiles.Core.Base;

    public interface ILayout<TTarget, TFieldSettings, TBuilder, out TLayout> : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
        where TBuilder : IFieldSettingsBuilder<TBuilder, TFieldSettings>
        where TLayout : ILayout<TTarget, TFieldSettings, TBuilder, TLayout>
    {
        TLayout WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TBuilder> settings = null);

        TLayout WithHeader();
    }
}