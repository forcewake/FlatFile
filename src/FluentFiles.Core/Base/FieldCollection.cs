namespace FluentFiles.Core.Base
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class AutoOrderedFieldsCollection<TFieldSettings> : FieldCollection<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        private int _currentPropertyId;

        public override void AddOrUpdate(TFieldSettings settings)
        {
            settings.Index = _currentPropertyId++;

            base.AddOrUpdate(settings);
        }
    }

    public class FieldCollection<TFieldSettings> : IFieldCollection<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        private IDictionary<string, (int Index, TFieldSettings Settings)> _fields;

        public FieldCollection()
        {
            _fields = new Dictionary<string, (int Index, TFieldSettings Settings)>();
        }

        public virtual void AddOrUpdate(TFieldSettings settings)
        {
            _fields[settings.UniqueKey] = (settings.Index.GetValueOrDefault(), settings);
        }

        public IEnumerator<TFieldSettings> GetEnumerator() =>
            _fields.Values
                   .OrderBy(x => x.Index)
                   .Select(x => x.Settings)
                   .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}