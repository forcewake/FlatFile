namespace FlatFile.Core.Base
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using FlatFile.Core.Extensions;

    public abstract class LayoutBase<TTarget, TFieldSettings, TConstructor, TLayout>
        : LayoutDescriptorBase<TFieldSettings>, ILayout<TTarget, TFieldSettings, TConstructor, TLayout>
        where TFieldSettings : class, IFieldSettings
        where TConstructor : IFieldSettingsConstructor<TConstructor> 
        where TLayout : ILayout<TTarget, TFieldSettings, TConstructor, TLayout>
    {
        private readonly IFieldSettingsFactory<TConstructor> _fieldSettingsFactory;

        protected LayoutBase(
            IFieldSettingsFactory<TConstructor> fieldSettingsFactory, 
            IFieldsContainer<TFieldSettings> fieldsContainer) : base(fieldsContainer)
        {
            this._fieldSettingsFactory = fieldSettingsFactory;
        }

        protected virtual void ProcessProperty<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TConstructor> settings)
        {
            var propertyInfo = GetPropertyInfo(expression);

            var constructor = _fieldSettingsFactory.CreateFieldSettings(propertyInfo);

            if (settings != null)
            {
                settings(constructor);
            }

            FieldsContainer.AddOrUpdate(constructor.PropertyInfo, constructor as TFieldSettings);
        }

        protected virtual PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TTarget, TProperty>> expression)
        {
            var propertyInfo = expression.GetPropertyInfo();
            return propertyInfo;
        }

        public Type TargetType { get { return typeof (TTarget); } }

        public abstract TLayout WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TConstructor> settings = null);

        public abstract TLayout WithHeader();
    }
}