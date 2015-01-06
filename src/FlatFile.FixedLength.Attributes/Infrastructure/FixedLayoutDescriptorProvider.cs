namespace FlatFile.FixedLength.Attributes.Infrastructure
{
    using System;
    using System.Linq;
    using FlatFile.Core;
    using FlatFile.Core.Attributes.Extensions;
    using FlatFile.Core.Attributes.Infrastructure;
    using FlatFile.Core.Base;

    public class FixedLayoutDescriptorProvider : ILayoutDescriptorProvider<FixedFieldSettings, ILayoutDescriptor<FixedFieldSettings>>
    {
        public ILayoutDescriptor<FixedFieldSettings> GetDescriptor<T>()
        {
            var container = new FieldsContainer<FixedFieldSettings>();

            var fileMappingType = typeof(T);

            var fileAttribute = fileMappingType.GetAttribute<FixedLengthFileAttribute>();

            if (fileAttribute == null)
            {
                throw new NotSupportedException(string.Format("Mapping type {0} should be marked with {1} attribute",
                    fileMappingType.Name,
                    typeof(FixedLengthFileAttribute).Name));
            }

            var properties = fileMappingType.GetTypeDescription<FixedLengthFieldAttribute>();

            foreach (var p in properties)
            {
                var attribute = p.Attributes.FirstOrDefault() as FixedLengthFieldAttribute;

                if (attribute != null)
                {
                    var fieldSettings = attribute.GetFieldSettings(p.Property);

                    container.AddOrUpdate(fieldSettings, false);
                }
            }

            var descriptor = new LayoutDescriptorBase<FixedFieldSettings>(container)
            {
                HasHeader = false
            };

            return descriptor;
        }
    }
}