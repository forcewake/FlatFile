namespace FlatFile.Core.Base
{
    using System;

    public abstract class LineParserBase<TEntity, TLayoutDescriptor, TFieldSettings> : ILineParser<TEntity>
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings> 
        where TFieldSettings : FieldSettingsBase
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

        protected virtual object GetFieldValueFromString(TFieldSettings fieldSettingsBuilder, string memberValue)
        {
            if (fieldSettingsBuilder.IsNullable && memberValue.Equals(fieldSettingsBuilder.NullValue))
            {
                return null;
            }
            memberValue = TransformStringValue(fieldSettingsBuilder, memberValue);
            
            if (fieldSettingsBuilder.PropertyInfo.PropertyType.IsGenericType
                && fieldSettingsBuilder.PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Convert.ChangeType(memberValue, Nullable.GetUnderlyingType(fieldSettingsBuilder.PropertyInfo.PropertyType));
            }
            return Convert.ChangeType(memberValue, fieldSettingsBuilder.PropertyInfo.PropertyType);
        }

        protected virtual string TransformStringValue(TFieldSettings fieldSettingsBuilder, string memberValue)
        {
            return memberValue;
        }
    }
}