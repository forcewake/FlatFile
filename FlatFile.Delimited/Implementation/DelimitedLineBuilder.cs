namespace FlatFile.Delimited.Implementation
{
    using System.IO;
    using System.Linq;
    using FlatFile.Core.Base;

    public class DelimitedLineBuilder :
        LineBuilderBase<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>, 
        IDelimitedLineBuilder
    {
        public DelimitedLineBuilder(IDelimitedLayoutDescriptor descriptor)
            : base(descriptor)
        {
        }

        public override void BuildLine<T>(T entry, TextWriter writer)
        {
            foreach(var field in Descriptor.Fields)
            {
                GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null), writer);
            }
        }

        protected override void TransformFieldValue(IDelimitedFieldSettingsContainer field, string lineValue, TextWriter writer)
        {
            var quotes = Descriptor.Quotes;
            if (!string.IsNullOrEmpty(quotes))
            {
                writer.Write(quotes);
                writer.Write(lineValue);
                writer.Write(quotes);
            }
            else
            {
                writer.Write(lineValue);
            }
        }
    }
}