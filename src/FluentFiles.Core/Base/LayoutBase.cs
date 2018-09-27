namespace FluentFiles.Core.Base
{
    using FluentFiles.Core.Extensions;
    using System;
    using System.Linq.Expressions;

    public abstract class LayoutBase<TTarget, TFieldSettings, TBuilder, TLayout> : LayoutDescriptorBase<TFieldSettings>, ILayout<TTarget, TFieldSettings, TBuilder, TLayout>
        where TFieldSettings : class, IFieldSettings
        where TBuilder : IFieldSettingsBuilder<TBuilder, TFieldSettings> 
        where TLayout : ILayout<TTarget, TFieldSettings, TBuilder, TLayout>
    {
        private readonly IFieldSettingsBuilderFactory<TBuilder, TFieldSettings> _fieldBuilderFactory;

        protected LayoutBase(
            IFieldSettingsBuilderFactory<TBuilder, TFieldSettings> fieldBuilderFactory, 
            IFieldsContainer<TFieldSettings> fieldsContainer)
                : base(fieldsContainer)
        {
            _fieldBuilderFactory = fieldBuilderFactory;
            InstanceFactory = ReflectionHelper.CreateConstructor(TargetType);
        }

        protected virtual void ProcessProperty<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TBuilder> configure)
        {
            var property = expression.GetPropertyInfo();
            var builder = _fieldBuilderFactory.CreateBuilder<TTarget, TProperty>(property);

            configure?.Invoke(builder);

            var fieldSettings = builder.Build();

            FieldsContainer.AddOrUpdate(property, fieldSettings);
        }

        public override Type TargetType { get { return typeof (TTarget); } }

        public override Func<object> InstanceFactory { get; }

        public abstract TLayout WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TBuilder> configure = null);

        public abstract TLayout WithHeader();
    }
}