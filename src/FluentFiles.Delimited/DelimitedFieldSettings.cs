namespace FluentFiles.Delimited
{
    using System.Reflection;
    using FluentFiles.Core.Base;

    public interface IDelimitedFieldSettings : IFieldSettings
    {
        string Name { get; set; }
    }

    public interface IDelimitedFieldSettingsContainer : IDelimitedFieldSettings, IFieldSettingsContainer
    {
    }

    public class DelimitedFieldSettings : FieldSettingsBase, IDelimitedFieldSettingsContainer
    {
        public DelimitedFieldSettings(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }

        public DelimitedFieldSettings(PropertyInfo propertyInfo, IDelimitedFieldSettings settings)
            : base(propertyInfo, settings)
        {
            Name = settings.Name;
        }

        public string Name { get; set; }
    }
}