namespace FlatFile.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using FlatFile.Core.Base;

    public interface ILayout<T, TFieldSettings, TConstructor, out TLayout> 
        where TFieldSettings : FieldSettingsBase
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
        where TLayout : ILayout<T, TFieldSettings, TConstructor, TLayout>
    {
        TLayout WithMember<TProperty>(Expression<Func<T, TProperty>> expression, Action<TConstructor> settings = null);
        
        IEnumerable<TFieldSettings> Fields { get; }
    }
}