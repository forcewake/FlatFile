namespace FluentFiles.Core.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class AutoOrderedFieldsContainer<TFieldSettings> : FieldsContainer<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        private int _currentPropertyId = 0;

        public override void AddOrUpdate(PropertyInfo propertyInfo, TFieldSettings settings)
        {
            settings.Index = _currentPropertyId++;

            base.AddOrUpdate(propertyInfo, settings);
        }
    }

    public class FieldsContainer<TFieldSettings> : IFieldsContainer<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        protected Dictionary<PropertyInfo, PropertySettingsContainer<TFieldSettings>> Fields { get; private set; }

        public FieldsContainer()
        {
            Fields = new Dictionary<PropertyInfo, PropertySettingsContainer<TFieldSettings>>();
        }

        public virtual void AddOrUpdate(PropertyInfo propertyInfo, TFieldSettings settings)
        {
            var propertySettings = new PropertySettingsContainer<TFieldSettings>
            {
                PropertySettings = settings,
                Index = settings.Index.GetValueOrDefault()
            };

            Fields[propertyInfo] = propertySettings;
        }

        public virtual IEnumerable<TFieldSettings> OrderedFields
        {
            get
            {
                return Fields.Values
                    .OrderBy(settings => settings.Index)
                    .Select(x => x.PropertySettings)
                    .AsEnumerable();
            }
        }
    }
}