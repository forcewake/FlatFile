namespace FluentFiles.Core.Base
{
    using System.Collections.Generic;
    using System.Linq;

    public class AutoOrderedFieldsContainer<TFieldSettings> : FieldsContainer<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        private int _currentPropertyId = 0;

        public override void AddOrUpdate<TKey>(TKey key, TFieldSettings settings)
        {
            settings.Index = _currentPropertyId++;

            base.AddOrUpdate(key, settings);
        }
    }

    public class FieldsContainer<TFieldSettings> : IFieldsContainer<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        protected Dictionary<object, PropertySettingsContainer<TFieldSettings>> Fields { get; private set; }

        public FieldsContainer()
        {
            Fields = new Dictionary<object, PropertySettingsContainer<TFieldSettings>>();
        }

        public virtual void AddOrUpdate<TKey>(TKey key, TFieldSettings settings)
        {
            var propertySettings = new PropertySettingsContainer<TFieldSettings>
            {
                PropertySettings = settings,
                Index = settings.Index.GetValueOrDefault()
            };

            Fields[key] = propertySettings;
        }

        public virtual IEnumerable<TFieldSettings> OrderedFields
        {
            get
            {
                return Fields.Values
                    .OrderBy(settings => settings.Index)
                    .Select(x => x.PropertySettings);
            }
        }
    }
}