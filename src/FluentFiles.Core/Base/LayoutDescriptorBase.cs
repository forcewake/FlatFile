using FluentFiles.Core.Extensions;
using System;
using System.Collections.Generic;

namespace FluentFiles.Core.Base
{
    public class LayoutDescriptorBase<TFieldSettings> : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
    {
        protected IFieldsContainer<TFieldSettings> FieldsContainer { get; private set; }

        protected LayoutDescriptorBase(IFieldsContainer<TFieldSettings> fieldsContainer)
        {
            FieldsContainer = fieldsContainer;
        }

        public LayoutDescriptorBase(IFieldsContainer<TFieldSettings> fieldsContainer, Type targetType) 
            : this(fieldsContainer)
        {
            TargetType = targetType;
            InstanceFactory = ReflectionHelper.CreateConstructor(targetType);
        }

        public virtual Type TargetType { get; private set; }

        public IEnumerable<TFieldSettings> Fields => FieldsContainer.OrderedFields;

        public bool HasHeader { get; protected internal set; }

        public virtual Func<object> InstanceFactory { get; }
    }
}