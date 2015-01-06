namespace FlatFile.Core
{
    using System.Collections.Generic;
    using FlatFile.Core.Base;

    public interface ILayoutDescriptor<TFieldSettings> 
        where TFieldSettings : FieldSettingsBase
    {
        IEnumerable<TFieldSettings> Fields { get; }

        bool HasHeader { get; }
    }
}