namespace FlatFile.Core
{
    using System;
    using FlatFile.Core.Base;

    public interface IFlatFileEngineFactory<TFieldSettings>
        where TFieldSettings : FieldSettingsBase
    {
        IFlatFileEngine<T> GetEngine<T>(
            ILayoutDescriptor<TFieldSettings> descriptor,
            Func<string, Exception, bool> handleEntryReadError = null) where T : class, new();
    }
}