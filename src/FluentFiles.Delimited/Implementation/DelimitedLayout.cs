namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    public class DelimitedLayout<TTarget> :
        LayoutBase<TTarget, IDelimitedFieldSettingsContainer, IDelimitedFieldSettingsBuilder, IDelimitedLayout<TTarget>>,
        IDelimitedLayout<TTarget>
    {
        public DelimitedLayout()
            : this(new DelimitedFieldSettingsBuilderFactory(),
                   new FieldsContainer<IDelimitedFieldSettingsContainer>())
        {
        }

        public DelimitedLayout(
            IFieldSettingsBuilderFactory<IDelimitedFieldSettingsBuilder, IDelimitedFieldSettingsContainer> fieldSettingsFactory,
            IFieldsContainer<IDelimitedFieldSettingsContainer> fieldsContainer)
                : base(fieldSettingsFactory, fieldsContainer)
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
            Action<IDelimitedFieldSettingsBuilder> settings = null)
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