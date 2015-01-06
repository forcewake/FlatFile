namespace FlatFile.Core.Base
{
    public abstract class LineBulderBase<TEntity, TLayoutDescriptor, TFieldSettings> : ILineBulder<TEntity>
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings> 
        where TFieldSettings : FieldSettingsBase 
    {
        private readonly TLayoutDescriptor _layout;

        protected LineBulderBase(TLayoutDescriptor layout)
        {
            this._layout = layout;
        }

        protected TLayoutDescriptor Layout
        {
            get { return _layout; }
        }

        public abstract string BuildLine(TEntity entry);

        protected virtual string GetStringValueFromField(TFieldSettings field, object fieldValue)
        {
            if (fieldValue == null)
            {
                return field.NullValue;
            }

            string lineValue = fieldValue.ToString();

            lineValue = TransformFieldValue(field, lineValue);

            return lineValue;
        }

        protected virtual string TransformFieldValue(TFieldSettings field, string lineValue)
        {
            return lineValue;
        }
    }
}