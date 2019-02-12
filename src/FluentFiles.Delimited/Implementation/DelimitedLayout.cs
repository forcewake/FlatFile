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
                   new FieldCollection<IDelimitedFieldSettingsContainer>())
        {
        }

        public DelimitedLayout(
            IFieldSettingsBuilderFactory<IDelimitedFieldSettingsBuilder, IDelimitedFieldSettingsContainer> fieldSettingsFactory,
            IFieldCollection<IDelimitedFieldSettingsContainer> fieldsContainer)
                : base(fieldSettingsFactory, fieldsContainer)
        {
            Quotes = string.Empty;
            Delimiter = ",";
        }

        public override IDelimitedLayout<TTarget> WithMember<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            Action<IDelimitedFieldSettingsBuilder> configure = null)
        {
            ProcessProperty(expression, configure);

            return this;
        }

        public override IDelimitedLayout<TTarget> WithHeader()
        {
            HasHeader = true;

            return this;
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
    }
}