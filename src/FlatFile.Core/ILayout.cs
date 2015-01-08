namespace FlatFile.Core
{
    using System;
    using System.Linq.Expressions;
    using FlatFile.Core.Base;

    public interface ILayout<T, TFieldSettings, TConstructor, out TLayout> : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
        where TConstructor : IFieldSettingsConstructor<TConstructor>
        where TLayout : ILayout<T, TFieldSettings, TConstructor, TLayout>
    {
        TLayout WithMember<TProperty>(Expression<Func<T, TProperty>> expression, Action<TConstructor> settings = null);

        TLayout WithHeader();
    }
}