namespace FlatFile.FixedLength.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLayout<TTarget> :
        LayoutBase<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>,
        IFixedLayout<TTarget>
    {
        public FixedLayout()
            : this(
                new FixedFieldSettingsFactory(),
                new FixedFieldSettingsBuilder(),
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

        public override IFixedLayout<TTarget> WithMember<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            Action<IFixedFieldSettingsConstructor> settings = null)
        {
            ProcessProperty(expression, settings);

            return this;
        }

        public override IFixedLayout<TTarget> WithHeader()
        {
            HasHeader = true;

            return this;
        }
    }
}