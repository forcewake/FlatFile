namespace FlatFile.Core.Attributes.Base
{
    using System;
    using FlatFile.Core.Base;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class FieldSettingsBaseAttribute : Attribute, IFieldSettings
    {
        public int? Index { get; set; }

        public bool IsNullable
        {
            get { return !string.IsNullOrEmpty(NullValue); }
        }

        public string NullValue { get; set; }

        protected FieldSettingsBaseAttribute(int index)
        {
            Index = index;
        }
    }
}