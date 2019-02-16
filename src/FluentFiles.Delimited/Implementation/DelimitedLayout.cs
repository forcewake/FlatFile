namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.Linq.Expressions;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    /// <summary>
    /// A mapping from a delimited file record to a target type.
    /// </summary>
    /// <typeparam name="TTarget">The type that a record maps to.</typeparam>
    public class DelimitedLayout<TTarget> :
        LayoutBase<TTarget, IDelimitedFieldSettingsContainer, IDelimitedFieldSettingsBuilder, IDelimitedLayout<TTarget>>,
        IDelimitedLayout<TTarget>
    {
        /// <summary>
        /// Initializes a new <see cref="DelimitedLayout{TTarget}"/>.
        /// </summary>
        public DelimitedLayout()
            : this(new DelimitedFieldSettingsBuilderFactory(),
                   new FieldCollection<IDelimitedFieldSettingsContainer>())
        {
        }

        /// <summary>
        /// Initializes a new <see cref="DelimitedLayout{TTarget}"/>.
        /// </summary>
        /// <param name="fieldSettingsFactory">Creates delimited field configurations.</param>
        /// <param name="fieldsContainer">Stores the field configurations in a layout.</param>
        public DelimitedLayout(
            IFieldSettingsBuilderFactory<IDelimitedFieldSettingsBuilder, IDelimitedFieldSettingsContainer> fieldSettingsFactory,
            IFieldCollection<IDelimitedFieldSettingsContainer> fieldsContainer)
                : base(fieldSettingsFactory, fieldsContainer)
        {
            Quotes = string.Empty;
            Delimiter = ",";
        }

        /// <summary>
        /// Configures a mapping from a record field to a member of a type.
        /// </summary>
        /// <typeparam name="TProperty">The type of the member a field maps to.</typeparam>
        /// <param name="expression">An expression selecting the member to map to.</param>
        /// <param name="configure">An action that performs configuration of a field mapping.</param>
        public override IDelimitedLayout<TTarget> WithMember<TProperty>(
            Expression<Func<TTarget, TProperty>> expression,
            Action<IDelimitedFieldSettingsBuilder> configure = null)
        {
            ProcessProperty(expression, configure);

            return this;
        }

        /// <summary>
        /// Indicates that a record layout contains a header.
        /// </summary>
        public override IDelimitedLayout<TTarget> WithHeader()
        {
            HasHeader = true;

            return this;
        }

        /// <summary>
        /// The delimiter used to separate fields in a record.
        /// </summary>
        public string Delimiter { get; private set; }

        /// <summary>
        /// The string used to quote delimited fields.
        /// </summary>
        public string Quotes { get; private set; }

        /// <summary>
        /// Specifies the string used to quote delimited fields.
        /// </summary>
        /// <param name="quote">The quote string.</param>
        public IDelimitedLayout<TTarget> WithQuote(string quote)
        {
            Quotes = quote;

            return this;
        }

        /// <summary>
        /// Specifies the delimiter used to separate fields in a record.
        /// </summary>
        /// <param name="delimiter">The delimiter string.</param>
        public IDelimitedLayout<TTarget> WithDelimiter(string delimiter)
        {
            Delimiter = delimiter;

            return this;
        }
    }
}