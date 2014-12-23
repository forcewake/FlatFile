namespace FlatFile.Delimited.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class DelimitedLayout<TTarget> :
        LayoutBase<TTarget, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor, IDelimitedLayout<TTarget>>,
        IDelimitedLayout<TTarget>
    {
        public DelimitedLayout()
            : this(
                new DelimitedFieldSettingsFactory(), new DelimitedFieldSettingsBuilder(),
                new FieldsContainer<DelimitedFieldSettings>())
        {
        }

        public DelimitedLayout(
            IFieldSettingsFactory<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor> fieldSettingsFactory,
            IFieldSettingsBuilder<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor> builder,
            IFieldsContainer<DelimitedFieldSettings> fieldsContainer)
            : base(fieldSettingsFactory, builder, fieldsContainer)
        {
            Quotes = string.Empty;
            Delimiter = ",";
        }

        public string Delimiter { get; private set; }

        public string Quotes { get; private set; }

        public IDelimitedLayout<TTarget> WithQuote(string quote)
        {
            Quotes = quote;

            return this;
        }

        public IDelimitedLayout<TTarget> WithDelimiter(string delimiter)
        {
            Delimiter = delimiter;

            return this;
        }

        public override IDelimitedLayout<TTarget> WithMember<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            Action<IDelimitedFieldSettingsConstructor> settings = null)
        {
            ProcessProperty(expression, settings);

            return this;
        }

        public override IDelimitedLayout<TTarget> WithHeader()
        {
            HasHeader = true;

            return this;
        }
    }
}