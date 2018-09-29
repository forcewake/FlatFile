namespace FluentFiles.Core
{
    using System.Collections.Generic;
    using FluentFiles.Core.Base;

    public interface IFieldsContainer<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        void AddOrUpdate(TFieldSettings settings);

        IEnumerable<TFieldSettings> OrderedFields { get; }
    }
}