namespace FlatFile.Delimited.Implementation
{
    using System.Linq;
    using FlatFile.Core.Base;

    public class DelimitedLineBuilder<TEntry> : 
        LineBulderBase<TEntry, IDelimitedLayout<TEntry>, DelimitedFieldSettings>, 
        IDelimitedLineBuilder<TEntry>
    {
        public DelimitedLineBuilder(IDelimitedLayout<TEntry> layout)
            : base(layout)
        {
        }

        public override string BuildLine(TEntry entry)
        {
            string line = Layout.Fields.Aggregate(string.Empty,
                (current, field) =>
                    current + (current.Length > 0 ? Layout.Delimiter : "") +
                    GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null)));
            return line;
        }

        protected override string TransformFieldValue(DelimitedFieldSettings field, string lineValue)
        {
            var quotes = Layout.Quotes;
            if (!string.IsNullOrEmpty(quotes))
            {
                lineValue = string.Format("{0}{1}{0}", quotes, lineValue);
            }
            return lineValue;
        }
    }
}