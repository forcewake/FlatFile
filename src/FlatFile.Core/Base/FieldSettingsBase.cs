namespace FlatFile.Core.Base
{
    using System.Reflection;

    public abstract class FieldSettingsBase
    {
        public bool IsNullable { get; set; }
        public string NullValue { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}