using System;

namespace FluentFiles.Core
{
    using System.Collections.Generic;
    using FluentFiles.Core.Base;

    public interface ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        Type TargetType { get; }

        IEnumerable<TFieldSettings> Fields { get; }

        bool HasHeader { get; }

        Func<object> InstanceFactory { get; }
    }
}