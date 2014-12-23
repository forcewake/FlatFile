namespace FlatFile.FixedLength.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLayout<TTarget> :
        LayoutBase<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, FixedLayout<TTarget>>,
        IFixedLayout<TTarget, FixedLayout<TTarget>>,
        IFixedLayout<TTarget>
    {
        public FixedLayout()
            : this(
                new FixedFieldSettingsFactory(), new FixedFieldSettingsBuilder(),
                new FieldsContainer<FixedFieldSettings>())
        {
        }

        public FixedLayout(
            IFieldSettingsFactory<FixedFieldSettings, IFixedFieldSettingsConstructor> fieldSettingsFactory,
            IFieldSettingsBuilder<FixedFieldSettings, IFixedFieldSettingsConstructor> builder,
            IFieldsContainer<FixedFieldSettings> fieldsContainer)
            : base(fieldSettingsFactory, builder, fieldsContainer)
        {
        }

        public override FixedLayout<TTarget> WithMember<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            Action<IFixedFieldSettingsConstructor> settings = null)
        {
            ProcessProperty(expression, settings);

            return this;
        }

        IFixedLayout<TTarget>
            ILayout<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>.WithHeader()
        {
            return WithHeader();
        }

        public override FixedLayout<TTarget> WithHeader()
        {
            HasHeader = true;
            return this;
        }

        IFixedLayout<TTarget>
            ILayout<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>.WithMember
            <TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<IFixedFieldSettingsConstructor> settings)
        {
            return WithMember(expression, settings);
        }
    }
}