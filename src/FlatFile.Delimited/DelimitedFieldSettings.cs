namespace FlatFile.Delimited
{
    using System.Reflection;
    using FlatFile.Core.Base;

    public interface IDelimitedFieldSettings : IFieldSettingsContainer
    {
        string Name { get; set; }
    }

    public class DelimitedFieldSettings : FieldSettingsBase, IDelimitedFieldSettings
    {
        public DelimitedFieldSettings(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public DelimitedFieldSettings(IDelimitedFieldSettings settings)
            : base(settings)
        {
            Name = settings.Name;
        }

        public DelimitedFieldSettings(PropertyInfo propertyInfo, IDelimitedFieldSettings settings)
            : this(settings)
        {
            PropertyInfo = propertyInfo;
        }

        public string Name { get; set; }
    }
}