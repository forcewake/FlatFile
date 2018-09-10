namespace FluentFiles.FixedLength.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;

    public class FixedFieldSettingsBuilderFactory : IFieldSettingsBuilderFactory<IFixedFieldSettingsBuilder, IFixedFieldSettingsContainer>
    {
        public IFixedFieldSettingsBuilder CreateBuilder<TTarget, TProperty>(PropertyInfo property) => new FixedFieldSettingsBuilder(property);
    }
}