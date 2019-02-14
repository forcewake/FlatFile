namespace FluentFiles.Core.Base
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An <see cref="IFieldCollection{TFieldSettings}"/> that automatically preserves insertion order.
    /// </summary>
    /// <typeparam name="TFieldSettings"></typeparam>
    public class AutoOrderedFieldsCollection<TFieldSettings> : FieldCollection<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        private int _currentPropertyId;

        /// <summary>
        /// Adds a new field configuration to the collection, or, if an existing configuration
        /// exists that matches the provided configuration, replaces it.
        /// </summary>
        /// <param name="settings">The field configueration to add.</param>
        public override void AddOrUpdate(TFieldSettings settings)
        {
            settings.Index = _currentPropertyId++;

            base.AddOrUpdate(settings);
        }
    }

    /// <summary>
    /// A collection of <see cref="IFieldSettings"/>.
    /// </summary>
    /// <typeparam name="TFieldSettings">The type of field configuration.</typeparam>
    public class FieldCollection<TFieldSettings> : IFieldCollection<TFieldSettings>
        where TFieldSettings : IFieldSettingsContainer
    {
        private IDictionary<string, (int Index, TFieldSettings Settings)> _fields;

        /// <summary>
        /// Initializes a new <see cref="FieldCollection{TFieldSettings}"/>.
        /// </summary>
        public FieldCollection()
        {
            _fields = new Dictionary<string, (int Index, TFieldSettings Settings)>();
        }

        /// <summary>
        /// Adds a new field configuration to the collection, or, if an existing configuration
        /// exists that matches the provided configuration, replaces it.
        /// </summary>
        /// <param name="settings">The field configueration to add.</param>
        public virtual void AddOrUpdate(TFieldSettings settings)
        {
            _fields[settings.UniqueKey] = (settings.Index.GetValueOrDefault(), settings);
        }

        /// <summary>
        /// Provides an ordered enumerator over the field configurations in the collection.
        /// </summary>
        public IEnumerator<TFieldSettings> GetEnumerator() =>
            _fields.Values
                   .OrderBy(x => x.Index)
                   .Select(x => x.Settings)
                   .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}