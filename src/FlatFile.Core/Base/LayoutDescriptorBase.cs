using System.Collections.Generic;

namespace FlatFile.Core.Base
{
    public class LayoutDescriptorBase<TFieldSettings> : ILayoutDescriptor<TFieldSettings> 
        where TFieldSettings : FieldSettingsBase
    {
        protected IFieldsContainer<TFieldSettings> FieldsContainer { get; private set; }

        public LayoutDescriptorBase(IFieldsContainer<TFieldSettings> fieldsContainer)
        {
            FieldsContainer = fieldsContainer;
        }

        public IEnumerable<TFieldSettings> Fields
        {
            get { return FieldsContainer.OrderedFields; }
        }

        public bool HasHeader { get; protected set; }
    }
}