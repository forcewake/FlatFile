namespace FlatFile.Core.Base
{
    using System.Reflection;

    public abstract class FieldSettingsBase
    {
        public int Id { get; set; }
        public bool IsNullable { get; set; }
        public string NullValue { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}