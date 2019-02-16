namespace FluentFiles.Delimited.Implementation
{
    using System.Reflection;
    using FluentFiles.Core.Base;

    internal class DelimitedFieldSettings : FieldSettingsBase, IDelimitedFieldSettingsContainer
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