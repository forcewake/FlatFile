namespace FlatFile.FixedLength.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using FlatFile.Core;
    using FlatFile.Core.Attributes.Extensions;
    using FlatFile.Core.Base;

    public static class FlatFileEngineFactoryExtensions
    {
        public static IFlatFileEngine<T> GetEngine<T>(
            this IFlatFileEngineFactory<FixedFieldSettings> engineFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            where T : class, new()
        {
            var container = new FieldsContainer<FixedFieldSettings>();

            var fileMappingType = typeof (T);
            
            var fileAttribute = fileMappingType.GetAttribute<FixedLengthFileAttribute>();
            
            if (fileAttribute == null)
            {
                throw new NotSupportedException(string.Format("Mapping type {0} should be marked with {1} attribute",
                    fileMappingType.Name,
                    typeof (FixedLengthFileAttribute).Name));
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

            var descriptor = new LayoutDescriptorBase<FixedFieldSettings>(container);

            return engineFactory.GetEngine<T>(descriptor, handleEntryReadError);
        }
    }

    public class PropertyDescription
    {
        public PropertyInfo Property { get; set; }
        public Attribute[] Attributes { get; set; }
    }

    public static class TypeExtensions
    {
        public static IEnumerable<PropertyDescription> GetTypeDescription<TAttribute>(this Type targetType) where TAttribute : Attribute
        {
            var properties = from p in targetType.GetProperties()
                             where Attribute.IsDefined(p, typeof(TAttribute))
                             let attr = p.GetCustomAttributes(typeof(TAttribute), true)
                             select new PropertyDescription { Property = p, Attributes = attr.Cast<Attribute>().ToArray() };

            return properties;
        }
    }
}