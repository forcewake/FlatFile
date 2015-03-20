namespace FlatFile.Core
{
    using System;
    using System.Linq.Expressions;
    using FlatFile.Core.Base;

    public interface ILayout<TTarget, TFieldSettings, TConstructor, out TLayout> : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
        where TConstructor : IFieldSettingsConstructor<TConstructor>
        where TLayout : ILayout<TTarget, TFieldSettings, TConstructor, TLayout>
    {
        Type TargetType { get; }

        TLayout WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TConstructor> settings = null);

        TLayout WithHeader();
    }
}