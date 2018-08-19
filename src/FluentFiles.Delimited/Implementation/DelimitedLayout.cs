namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    public class DelimitedLayout<TTarget> :
        LayoutBase<TTarget, IDelimitedFieldSettingsContainer, IDelimitedFieldSettingsConstructor, IDelimitedLayout<TTarget>>,
        IDelimitedLayout<TTarget>
    {
        public DelimitedLayout()
            : this(
                new DelimitedFieldSettingsFactory(),
                new FieldsContainer<IDelimitedFieldSettingsContainer>())
        {
        }

        public DelimitedLayout(
            IFieldSettingsFactory<IDelimitedFieldSettingsConstructor> fieldSettingsFactory,
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