namespace FluentFiles.FixedLength.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    public class FixedLayout<TTarget> :
        LayoutBase<TTarget, IFixedFieldSettingsContainer, IFixedFieldSettingsBuilder, IFixedLayout<TTarget>>,
        IFixedLayout<TTarget>
    {
        public FixedLayout()
            : this(new FixedFieldSettingsBuilderFactory(),
                   new FieldsContainer<IFixedFieldSettingsContainer>())
        {
        }

        public FixedLayout(
            IFieldSettingsBuilderFactory<IFixedFieldSettingsBuilder, IFixedFieldSettingsContainer> fieldSettingsFactory,
            IFieldsContainer<IFixedFieldSettingsContainer> fieldsContainer)
                : base(fieldSettingsFactory, fieldsContainer)
        {
        }

        public override IFixedLayout<TTarget> WithMember<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            Action<IFixedFieldSettingsBuilder> configure = null)
        {
            ProcessProperty(expression, configure);

            return this;
        }

        public override IFixedLayout<TTarget> WithHeader()
        {
            HasHeader = true;

            return this;
        }

        public IFixedLayout<TTarget> Ignore(int length)
        {
            FieldsContainer.AddOrUpdate(new IgnoredFixedFieldSettings(length));
            return this;
        }
    }
}