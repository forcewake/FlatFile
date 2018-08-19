namespace FluentFiles.Core
{
    using System;
    using FluentFiles.Core.Base;

    /// <summary>
    /// Interface IFlatFileEngineFactory
    /// </summary>
    /// <typeparam name="TDescriptor">The type of the t descriptor.</typeparam>
    /// <typeparam name="TFieldSettings">The type of the t field settings.</typeparam>
    public interface IFlatFileEngineFactory<in TDescriptor, TFieldSettings>
        where TDescriptor : ILayoutDescriptor<TFieldSettings> 
        where TFieldSettings : IFieldSettings
    {
        /// <summary>
        /// Gets the <see cref="IFlatFileEngine"/>.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="handleEntryReadError">The handle entry read error func.</param>
        /// <returns>IFlatFileEngine.</returns>
        IFlatFileEngine GetEngine(TDescriptor descriptor, Func<string, Exception, bool> handleEntryReadError = null);
    }
}