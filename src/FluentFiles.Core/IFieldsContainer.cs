namespace FluentFiles.Core
{
    using System.Collections.Generic;
    using System.Reflection;
    using FluentFiles.Core.Base;

    public interface IFieldsContainer<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        void AddOrUpdate(PropertyInfo propertyInfo, TFieldSettings settings);

        IEnumerable<TFieldSettings> OrderedFields { get; }
    }
}