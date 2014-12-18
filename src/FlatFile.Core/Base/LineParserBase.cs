namespace FlatFile.Core.Base
{
    using System;

    public abstract class LineParserBase<TEntity, TLayout, TFieldSettings, TConstructor> : ILineParser<TEntity>
        where TLayout : ILayout<TEntity, TFieldSettings, TConstructor, TLayout>
        where TFieldSettings : FieldSettingsBase
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
    {
        private readonly TLayout _layout;

        protected LineParserBase(TLayout layout)
        {
            this._layout = layout;
        }

        protected TLayout Layout
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