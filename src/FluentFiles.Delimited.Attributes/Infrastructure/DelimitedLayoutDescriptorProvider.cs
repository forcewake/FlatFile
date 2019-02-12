namespace FluentFiles.Delimited.Attributes.Infrastructure
{
    using System;
    using System.Linq;
    using FluentFiles.Core;
    using FluentFiles.Core.Attributes.Extensions;
    using FluentFiles.Core.Attributes.Infrastructure;
    using FluentFiles.Core.Base;
    using FluentFiles.Delimited.Implementation;

    internal class DelimitedLayoutDescriptorProvider : ILayoutDescriptorProvider<IDelimitedFieldSettingsContainer, IDelimitedLayoutDescriptor>
    {
        public IDelimitedLayoutDescriptor GetDescriptor<T>() => GetDescriptor(typeof(T));

        public IDelimitedLayoutDescriptor GetDescriptor(Type t)
        {
            var container = new FieldCollection<IDelimitedFieldSettingsContainer>();

            var fileMappingType = t;

            var fileAttribute = fileMappingType.GetAttribute<DelimitedFileAttribute>();
            if (fileAttribute == null)
            {
                throw new NotSupportedException(string.Format("Mapping type {0} should be marked with {1} attribute",
                    fileMappingType.Name,
                    typeof(DelimitedFileAttribute).Name));
            }

            var properties = fileMappingType.GetTypeDescription<DelimitedFieldAttribute>();
            foreach (var p in properties)
            {
                var settings = p.Attributes.FirstOrDefault() as IDelimitedFieldSettings;
                if (settings != null)
                {
                    container.AddOrUpdate(new DelimitedFieldSettings(p.Property, settings));
                }
            }
            
            var descriptor = new DelimitedLayoutDescriptor(container, fileMappingType, fileAttribute);            
            return descriptor;
        }

        private sealed class DelimitedLayoutDescriptor : LayoutDescriptorBase<IDelimitedFieldSettingsContainer>, IDelimitedLayoutDescriptor
        {
            public DelimitedLayoutDescriptor(
                IFieldCollection<IDelimitedFieldSettingsContainer> fieldsContainer,
                Type targetType,
                DelimitedFileAttribute fileAttribute)
                    : base(fieldsContainer, targetType)
            {
                HasHeader = fileAttribute.HasHeader;
                Delimiter = fileAttribute.Delimiter ?? ",";
                Quotes = fileAttribute.Quotes ?? string.Empty;
            }

            public string Delimiter { get; }

            public string Quotes { get; }
        }
    }
}
