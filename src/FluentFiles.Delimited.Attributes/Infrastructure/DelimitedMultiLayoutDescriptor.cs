namespace FluentFiles.Delimited.Attributes.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using FluentFiles.Core;
    using FluentFiles.Core.Attributes.Infrastructure;

    public class DelimitedMultiLayoutDescriptor<TFieldSettings> : ILayoutDescriptorProvider<TFieldSettings, ILayoutDescriptor<TFieldSettings>>
        where TFieldSettings : IDelimitedFieldSettingsContainer
    {
        protected IFieldCollection<TFieldSettings> FieldCollection { get; }

        public DelimitedMultiLayoutDescriptor(IFieldCollection<TFieldSettings> fieldCollection)
        {
            FieldCollection = fieldCollection;
        }

        public DelimitedMultiLayoutDescriptor(IFieldCollection<TFieldSettings> fieldsContainer, Type targetType) : this(fieldsContainer) { TargetType = targetType; }

        ILayoutDescriptor<TFieldSettings> ILayoutDescriptorProvider<TFieldSettings, ILayoutDescriptor<TFieldSettings>>.GetDescriptor<T>()
        {
            throw new NotImplementedException();
        }

        public virtual Type TargetType { get; }

        public IEnumerable<TFieldSettings> Fields => FieldCollection;

        public bool HasHeader { get; protected internal set; }

        public string Delimiter { get; private set; }

        public string Quotes { get; private set; }

        public DelimitedMultiLayoutDescriptor<TFieldSettings> WithQuote(string quote)
        {
            Quotes = quote;
            return this;
        }

        public DelimitedMultiLayoutDescriptor<TFieldSettings> WithDelimiter(string delimiter)
        {
            Delimiter = delimiter;
            return this;
        }
    }
}