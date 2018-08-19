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
        public DelimitedFieldSettings(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public DelimitedFieldSettings(IDelimitedFieldSettings settings)
            : base(settings)
        {
            Name = settings.Name;
			TypeConverter = settings.TypeConverter;
        }

        public DelimitedFieldSettings(PropertyInfo propertyInfo, IDelimitedFieldSettings settings)
            : this(settings)
        {
            PropertyInfo = propertyInfo;
        }

        public string Name { get; set; }
    }
}