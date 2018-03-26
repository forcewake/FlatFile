using System;
using System.Collections.Generic;

namespace FlatFile.Core.Base
{
    public class LayoutDescriptorBase<TFieldSettings> : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        protected IFieldsContainer<TFieldSettings> FieldsContainer { get; private set; }

        public LayoutDescriptorBase(IFieldsContainer<TFieldSettings> fieldsContainer)
        {
            FieldsContainer = fieldsContainer;
        }

        public LayoutDescriptorBase(IFieldsContainer<TFieldSettings> fieldsContainer, Type targetType) : this(fieldsContainer) { TargetType = targetType; }

        public virtual Type TargetType { get; private set; }

        public IEnumerable<TFieldSettings> Fields
        {
            get { return FieldsContainer.OrderedFields; }
        }

        public bool HasHeader { get; protected internal set; }
    }
}
