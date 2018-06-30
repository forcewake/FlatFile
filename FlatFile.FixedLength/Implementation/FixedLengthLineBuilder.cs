namespace FlatFile.FixedLength.Implementation
{
    using System.Collections.Concurrent;
    using System.IO;
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

        public override void BuildLine<T>(T entry, TextWriter writer)
        {
            foreach (var field in Descriptor.Fields)
            {
                GetStringValueFromField(field, field.PropertyInfo.GetValue(entry, null), writer);
            }
        }

        private static ConcurrentDictionary<char, ConcurrentDictionary<int, string>> _paddings = new ConcurrentDictionary<char, ConcurrentDictionary<int, string>>();

        private static string getPadding(char character, int length)
        {
            var padDict = _paddings.GetOrAdd(character, new ConcurrentDictionary<int, string>());
            var padding = padDict.GetOrAdd(length, string.Join("", Enumerable.Repeat(character, length)));
            return padding;
        }

        protected override void TransformFieldValue(IFixedFieldSettingsContainer field, string lineValue, TextWriter writer)
        {
            if (field.StringNormalizer != null)
            {
                lineValue = field.StringNormalizer(lineValue);
            }

            if (lineValue.Length > field.Length)
            {
                writer.Write(field.TruncateIfExceedFieldLength ? lineValue.Substring(0, field.Length) : lineValue);
            }
            else if (lineValue.Length == field.Length)
            {
                writer.Write( lineValue);
            }
            else
            {
                var padding = getPadding(field.PaddingChar, field.Length - lineValue.Length);
                if (field.PadLeft)
                {
                    writer.Write(padding);
                    writer.Write(lineValue);
                }
                else
                {
                    writer.Write(lineValue);
                    writer.Write(padding);
                }
            }
        }
    }
}