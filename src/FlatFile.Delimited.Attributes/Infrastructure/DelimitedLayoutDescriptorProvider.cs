namespace FlatFile.Delimited.Attributes.Infrastructure
{
    using System;
    using System.Linq;
    using FlatFile.Core.Attributes.Extensions;
    using FlatFile.Core.Attributes.Infrastructure;
    using FlatFile.Core.Base;
    using FlatFile.Delimited.Implementation;

    public class DelimitedLayoutDescriptorProvider : ILayoutDescriptorProvider<IDelimitedFieldSettingsContainer, IDelimitedLayoutDescriptor>
    {
        public IDelimitedLayoutDescriptor GetDescriptor<T>()
        {
            var container = new FieldsContainer<IDelimitedFieldSettingsContainer>();

            var fileMappingType = typeof (T);

            var fileAttribute = fileMappingType.GetAttribute<DelimitedFileAttribute>();

            if (fileAttribute == null)
            {
                throw new NotSupportedException(string.Format("Mapping type {0} should be marked with {1} attribute",
                    fileMappingType.Name,
                    typeof (DelimitedFileAttribute).Name));
            }

            var properties = fileMappingType.GetTypeDescription<DelimitedFieldAttribute>();

            foreach (var p in properties)
            {
                var settings = p.Attributes.FirstOrDefault() as IDelimitedFieldSettings;

                if (settings != null)
                {
                    container.AddOrUpdate(p.Property, new DelimitedFieldSettings(p.Property, settings));
                }
            }

            var descriptor = new DelimitedLayout<T>(new DelimitedFieldSettingsFactory(), container)
                .WithDelimiter(fileAttribute.Delimiter)
                .WithQuote(fileAttribute.Quotes);

            if (fileAttribute.HasHeader)
            {
                descriptor.WithHeader();
            }

            return descriptor;
        }
    }
}
