using System.IO;

namespace FlatFile.Core.Base
{
    public abstract class LineBuilderBase<TLayoutDescriptor, TFieldSettings> : ILineBuilder
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer 
    {
        private readonly TLayoutDescriptor _descriptor;

        protected LineBuilderBase(TLayoutDescriptor descriptor)
        {
            this._descriptor = descriptor;
        }

        protected TLayoutDescriptor Descriptor
        {
            get { return _descriptor; }
        }

        public abstract void BuildLine<T>(T entry, TextWriter writer);

        protected virtual void GetStringValueFromField(TFieldSettings field, object fieldValue, TextWriter writer)
        {
            string lineValue = fieldValue != null
                ? fieldValue.ToString()
                : field.NullValue ?? string.Empty;

            TransformFieldValue(field, lineValue, writer);
        }

        protected virtual void TransformFieldValue(TFieldSettings field, string lineValue, TextWriter writer)
        {
            writer.Write(lineValue);
        }
    }
}