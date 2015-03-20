namespace FlatFile.Delimited.Implementation
{
    using System.Linq;
    using FlatFile.Core.Base;

    public class DelimitedLineBuilder :
        LineBulderBase<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>, 
        IDelimitedLineBuilder
    {
        public DelimitedLineBuilder(IDelimitedLayoutDescriptor descriptor)
            : base(descriptor)
        {
        }

        public override string BuildLine<T>(T entry)
        {
            string line = Descriptor.Fields.Aggregate(string.Empty,
                (current, field) =>
                    current + (current.Length > 0 ? Descriptor.Delimiter : "") +
                    GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null)));
            return line;
        }

        protected override string TransformFieldValue(IDelimitedFieldSettingsContainer field, string lineValue)
        {
            var quotes = Descriptor.Quotes;
            if (!string.IsNullOrEmpty(quotes))
            {
                lineValue = string.Format("{0}{1}{0}", quotes, lineValue);
            }
            return lineValue;
        }
    }
}