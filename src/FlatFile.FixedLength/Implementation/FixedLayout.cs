namespace FlatFile.FixedLength.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLayout<TTarget> :
        LayoutBase<TTarget, IFixedFieldSettingsContainer, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>,
        IFixedLayout<TTarget>
    {
        public FixedLayout()
            : this(
                new FixedFieldSettingsFactory(),
                new FieldsContainer<IFixedFieldSettingsContainer>())
        {
        }

        public FixedLayout(
            IFieldSettingsFactory<IFixedFieldSettingsConstructor> fieldSettingsFactory,
            IFieldsContainer<IFixedFieldSettingsContainer> fieldsContainer)
            : base(fieldSettingsFactory, fieldsContainer)
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