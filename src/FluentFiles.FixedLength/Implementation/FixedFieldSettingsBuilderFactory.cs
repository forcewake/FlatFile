namespace FluentFiles.FixedLength.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;

    public class FixedFieldSettingsBuilderFactory : IFieldSettingsBuilderFactory<IFixedFieldSettingsBuilder, IFixedFieldSettingsContainer>
    {
        public IFixedFieldSettingsBuilder CreateBuilder(PropertyInfo property) => new FixedFieldSettingsBuilder(property);
    }
}