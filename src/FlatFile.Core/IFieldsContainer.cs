namespace FlatFile.Core
{
    using System.Linq;
    using FlatFile.Core.Base;

    public interface IFieldsContainer<TFieldSettings> 
        where TFieldSettings : FieldSettingsBase
    {
        void AddOrUpdate(TFieldSettings settings);

        IOrderedEnumerable<TFieldSettings> OrderedFields { get; }
    }
}