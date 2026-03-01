namespace FlatFile.Delimited.Implementation
{
    using System.Text;
    using FlatFile.Core.Base;
    using FlatFile.Core.Extensions;

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
            var delimiter = Descriptor.Delimiter;
            var lineBuilder = new StringBuilder();
            bool isFirst = true;

            foreach (var field in Descriptor.Fields)
            {
                if (!isFirst)
                {
                    lineBuilder.Append(delimiter);
                }

                lineBuilder.Append(GetStringValueFromField(field, PropertyAccessorCache.GetValue(field.PropertyInfo, entry)));
                isFirst = false;
            }

            return lineBuilder.ToString();
        }

        protected override string TransformFieldValue(IDelimitedFieldSettingsContainer field, string lineValue)
        {
            var quotes = Descriptor.Quotes;
            if (!string.IsNullOrEmpty(quotes))
            {
                return quotes + lineValue + quotes;
            }

            return lineValue;
        }
    }
}
