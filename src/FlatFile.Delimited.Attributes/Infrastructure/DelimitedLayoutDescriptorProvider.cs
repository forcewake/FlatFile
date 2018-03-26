namespace FlatFile.Delimited.Attributes.Infrastructure
{
    using System;
    using System.Linq;
    using System.Reflection;
    using FlatFile.Core;
    using FlatFile.Core.Attributes.Extensions;
    using FlatFile.Core.Attributes.Infrastructure;
    using FlatFile.Core.Base;
    using FlatFile.Delimited.Implementation;
    

    public class DelimitedLayoutDescriptorProvider : ILayoutDescriptorProvider<IDelimitedFieldSettingsContainer, IDelimitedLayoutDescriptor>
    {
        public IDelimitedLayoutDescriptor GetDescriptor<T>()
        {
            return GetDescriptor(typeof(T));
        }

        public IDelimitedLayoutDescriptor GetDescriptor(Type t)
        {
            var container = new FieldsContainer<IDelimitedFieldSettingsContainer>();

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
                    container.AddOrUpdate(p.Property, new DelimitedFieldSettings(p.Property, settings));
                }
            }
            

            var methodInfo = typeof(DelimedLayoutGeneric)
                            .GetTypeInfo()
                            .GetDeclaredMethod("GetDelimitedLayout")
                            .MakeGenericMethod(new[] { t });
            object[] args = { null, new DelimitedFieldSettingsFactory(), container, fileAttribute };
            var descriptor = (IDelimitedLayoutDescriptor)methodInfo.Invoke(null, args);
            
            //var descriptor = DelimedLayoutGeneric.GetDelimitedLayout(t, new DelimitedFieldSettingsFactory(), container)
            //    .WithDelimiter(fileAttribute.Delimiter)
            //    .WithQuote(fileAttribute.Quotes);

            //if (fileAttribute.HasHeader)
            //{
            //    descriptor.WithHeader();
            //}

            return descriptor;
        }
    }

    public class DelimedLayoutGeneric
    {
        public static IDelimitedLayoutDescriptor GetDelimitedLayout<TTarget>(TTarget t,
            IFieldSettingsFactory<IDelimitedFieldSettingsConstructor> fieldSettingsFactory,
            IFieldsContainer<IDelimitedFieldSettingsContainer> fieldsContainer,
            DelimitedFileAttribute fileAttribute)
        {
            var dl = new DelimitedLayout<TTarget>(fieldSettingsFactory, fieldsContainer)
                .WithDelimiter(fileAttribute.Delimiter)
                .WithQuote(fileAttribute.Quotes);

            if (fileAttribute.HasHeader)
            {
                dl.WithHeader();
            }
            
            return dl;
        }
    }
}
