namespace FlatFile.Core.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class FieldsContainer<TFieldSettings> : IFieldsContainer<TFieldSettings>
        where TFieldSettings : FieldSettingsBase
    {
        private readonly Dictionary<PropertyInfo, TFieldSettings> fields;
        private int currentPropertyId = 0;

        public FieldsContainer()
        {
            fields = new Dictionary<PropertyInfo, TFieldSettings>();
        }

        public void AddOrUpdate(TFieldSettings settings, bool autoIncrement = true)
        {
            if (autoIncrement)
            {
                settings.Index = currentPropertyId++;                
            }

            fields[settings.PropertyInfo] = settings;
        }

        public IOrderedEnumerable<TFieldSettings> OrderedFields
        {
            get { return fields.Values.OrderBy(settings => settings.Index); }
        }
    }
}