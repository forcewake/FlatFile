namespace FluentFiles.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// A collection of field settings.
    /// </summary>
    /// <typeparam name="TFieldSettings">The type of field mappings in the collection.</typeparam>
    public interface IFieldCollection<TFieldSettings> : IEnumerable<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        /// <summary>
        /// Adds a new field configuration or updates an existing one if a match is found.
        /// </summary>
        /// <param name="settings">The field mapping to add.</param>
        void AddOrUpdate(TFieldSettings settings);
    }
}