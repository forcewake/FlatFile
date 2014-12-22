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
        private string _delimiter;
        private string _quotes;

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
        }

        public string Delimiter
        {
            get { return _delimiter; }
        }

        public string Quotes
        {
            get { return _quotes; }
        }

        public IDelimitedLayout<TTarget, IDelimitedLayout<TTarget>> WithQuote(string quote)
        {
            _quotes = quote;
            return this;
        }

        public IDelimitedLayout<TTarget, IDelimitedLayout<TTarget>> WithDelimiter(string delimiter)
        {
            _delimiter = delimiter;
            return this;
        }

        public override IDelimitedLayout<TTarget> WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression,
            Action<IDelimitedFieldSettingsConstructor> settings = null)
        {
            ProcessProperty(expression, settings);
            return this;
        }
    }
}