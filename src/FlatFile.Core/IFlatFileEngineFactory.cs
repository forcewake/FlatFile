namespace FlatFile.Core
{
    using System;
    using FlatFile.Core.Base;

    public interface IFlatFileEngineFactory<in TDescriptor, TFieldSettings>
        where TDescriptor : ILayoutDescriptor<TFieldSettings> 
        where TFieldSettings : IFieldSettings
    {
        IFlatFileEngine<T> GetEngine<T>(
            TDescriptor descriptor,
            Func<string, Exception, bool> handleEntryReadError = null) where T : class, new();
    }
}