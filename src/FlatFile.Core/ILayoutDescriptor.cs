using System;

namespace FlatFile.Core
{
    using System.Collections.Generic;
    using FlatFile.Core.Base;

    public interface ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        Type TargetType { get; }

        IEnumerable<TFieldSettings> Fields { get; }

        bool HasHeader { get; }
    }
}