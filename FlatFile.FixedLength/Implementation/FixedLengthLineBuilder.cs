namespace FlatFile.FixedLength.Implementation
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLengthLineBuilder :
        LineBuilderBase<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>,
        IFixedLengthLineBuilder
    {
        public FixedLengthLineBuilder(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
            : base(descriptor)
        {
        }

        public override string BuildLine<T>(T entry)
        {
            var sb = new StringBuilder();
            foreach (var field in Descriptor.Fields)
            {
                sb.Append(GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null)));
            }
            return sb.ToString();
        }

        private static ConcurrentDictionary<char, ConcurrentDictionary<int, string>> _paddings = new ConcurrentDictionary<char, ConcurrentDictionary<int, string>>();

        private static string getPadding(char character, int length)
        {
            var padDict = _paddings.GetOrAdd(character, new ConcurrentDictionary<int, string>());
            var padding = padDict.GetOrAdd(length, string.Join("", Enumerable.Repeat(character, length)));
            return padding;
        }

        protected override string TransformFieldValue(IFixedFieldSettingsContainer field, string lineValue)
        {
            if (field.StringNormalizer != null)
            {
                lineValue = field.StringNormalizer(lineValue);
            }

            if (lineValue.Length > field.Length)
            {
                return field.TruncateIfExceedFieldLength ? lineValue.Substring(0, field.Length) : lineValue;
            }
            else if (lineValue.Length == field.Length)
            {
                return lineValue;
            }
            else
            {
                var padding = getPadding(field.PaddingChar, field.Length - lineValue.Length);
                if (field.PadLeft)
                {
                    return padding + lineValue;
                }
                else
                {
                    return lineValue + padding;
                }
            }
        }
    }
}