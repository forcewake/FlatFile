namespace FlatFile.Core.Base
{
    public abstract class LineBulderBase<TEntity, TLayout, TFieldSettings, TConstructor> : ILineBulder<TEntity>
        where TLayout : ILayout<TEntity, TFieldSettings, TConstructor, TLayout> 
        where TFieldSettings : FieldSettingsBase 
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
    {
        private readonly TLayout _layout;

        protected LineBulderBase(TLayout layout)
        {
            this._layout = layout;
        }

        protected TLayout Layout
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