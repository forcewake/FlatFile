namespace FlatFile.Core.Attributes.Base
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class FieldSettingsBaseAttribute : Attribute
    {
        public int Index { get; protected set; }

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