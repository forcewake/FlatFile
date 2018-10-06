namespace FluentFiles.Delimited.Implementation
{
    using System.Linq;
    using System.Text;
    using FluentFiles.Core.Base;

    public class DelimitedLineBuilder :
        LineBuilderBase<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>, 
        IDelimitedLineBuilder
    {
        private readonly StringBuilder _lineBuilder;

        public DelimitedLineBuilder(IDelimitedLayoutDescriptor descriptor)
            : base(descriptor)
        {
            _lineBuilder = new StringBuilder(Descriptor.Fields.Count() * 5);
        }

        public override string BuildLine<T>(T entry)
        {
            _lineBuilder.Clear();
            foreach (var field in Descriptor.Fields)
            {
                _lineBuilder.Append(_lineBuilder.Length > 0 ? Descriptor.Delimiter : string.Empty)
                            .Append(GetStringValueFromField(field, field.GetValueOf(entry)));
            }
            return _lineBuilder.ToString();
        }

        protected override string PostprocessFieldValue(IDelimitedFieldSettingsContainer field, string value)
        {
            var quotes = Descriptor.Quotes;
            if (!string.IsNullOrEmpty(quotes))
            {
                value = string.Format("{0}{1}{0}", quotes, value);
            }
            return value;
        }
    }
}