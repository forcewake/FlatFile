namespace FluentFiles.Delimited.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;

    public class DelimitedFieldSettingsBuilderFactory : IFieldSettingsBuilderFactory<IDelimitedFieldSettingsBuilder, IDelimitedFieldSettingsContainer>
    {
        public IDelimitedFieldSettingsBuilder CreateBuilder<TTarget, TProperty>(PropertyInfo property) => new DelimitedFieldSettingsBuilder(property);
    }
}