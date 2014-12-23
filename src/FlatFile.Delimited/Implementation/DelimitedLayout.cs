namespace FlatFile.Delimited.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class DelimitedLayout<TTarget> :
        LayoutBase<TTarget, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor, DelimitedLayout<TTarget>>,
        IDelimitedLayout<TTarget>,
        IDelimitedLayout<TTarget, DelimitedLayout<TTarget>>
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


        public DelimitedLayout<TTarget> WithQuote(string quote)
        {
            Quotes = quote;
            return this;
        }

        IDelimitedLayout<TTarget> IDelimitedLayout<TTarget, IDelimitedLayout<TTarget>>.WithDelimiter(string delimiter)
        {
            return WithDelimiter(delimiter);
        }

        IDelimitedLayout<TTarget> IDelimitedLayout<TTarget, IDelimitedLayout<TTarget>>.WithQuote(string quote)
        {
            return WithQuote(quote);
        }

        public DelimitedLayout<TTarget> WithDelimiter(string delimiter)
        {
            Delimiter = delimiter;
            return this;
        }

        public override DelimitedLayout<TTarget> WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression,
            Action<IDelimitedFieldSettingsConstructor> settings = null)
        {
            ProcessProperty(expression, settings);
            return this;
        }

        IDelimitedLayout<TTarget> ILayout<TTarget, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor, IDelimitedLayout<TTarget>>.WithHeader()
        {
            return WithHeader();
        }

        public override DelimitedLayout<TTarget> WithHeader()
        {
            HasHeader = true;
            return this;
        }

        IDelimitedLayout<TTarget>
            ILayout<TTarget, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor, IDelimitedLayout<TTarget>>.
            WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression,
                Action<IDelimitedFieldSettingsConstructor> settings)
        {
            return WithMember(expression, settings);
        }
    }
}