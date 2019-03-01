namespace FluentFiles.Delimited.Implementation
{
    using System.Reflection;
    using FluentFiles.Core.Base;

    internal class DelimitedFieldSettings : FieldSettingsBase, IDelimitedFieldSettingsContainer
    {
        public DelimitedFieldSettings(MemberInfo member)
            : base(member)
        {
        }

        public DelimitedFieldSettings(MemberInfo member, IDelimitedFieldSettings settings)
            : base(member, settings)
        {
            Name = settings.Name;
        }

        public string Name { get; set; }
    }
}