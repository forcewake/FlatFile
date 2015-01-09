namespace FlatFile.Core.Base
{
    using System;
    using FlatFile.Core.Extensions;

    public abstract class LineParserBase<TEntity, TLayoutDescriptor, TFieldSettings> : ILineParser<TEntity>
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        private readonly TLayoutDescriptor _layout;

        protected LineParserBase(TLayoutDescriptor layout)
        {
            this._layout = layout;
        }

        protected TLayoutDescriptor Layout
        {
            get { return _layout; }
        }

        public abstract TEntity ParseLine(string line, TEntity entry);

        protected virtual object GetFieldValueFromString(TFieldSettings fieldSettings, string memberValue)
        {
            if (fieldSettings.IsNullable && memberValue.Equals(fieldSettings.NullValue))
            {
                return null;
            }

            memberValue = TransformStringValue(fieldSettings, memberValue);

            var type = fieldSettings.PropertyInfo.PropertyType;

            if (fieldSettings.IsNullablePropertyType())
            {
                type = Nullable.GetUnderlyingType(fieldSettings.PropertyInfo.PropertyType);
            }

            return memberValue.Convert(type);
        }

        protected virtual string TransformStringValue(TFieldSettings fieldSettingsBuilder, string memberValue)
        {
            return memberValue;
        }
    }
}