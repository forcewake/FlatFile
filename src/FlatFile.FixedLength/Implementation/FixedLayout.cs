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
            : this(new FixedFieldSettingsFactory(), new FixedFieldSettingsBuilder())
        {
        }

        public FixedLayout(
            IFieldSettingsFactory<FixedFieldSettings, IFixedFieldSettingsConstructor> fieldSettingsFactory,
            IFieldSettingsBuilder<FixedFieldSettings, IFixedFieldSettingsConstructor> builder)
            : base(fieldSettingsFactory, builder)
        {
        }

        public override IFixedLayout<TTarget> WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression,
            Action<IFixedFieldSettingsConstructor> settings)
        {
            ProcessProperty(expression, settings);

            return this;
        }

        protected virtual void MapLayout()
        {
        }
    }
}