namespace FluentFiles.FixedLength.Attributes.Infrastructure
{
    using System;
    using System.Linq;
    using FluentFiles.Core;
    using FluentFiles.Core.Attributes.Extensions;
    using FluentFiles.Core.Attributes.Infrastructure;
    using FluentFiles.Core.Base;
    using FluentFiles.FixedLength.Implementation;

    internal class FixedLayoutDescriptorProvider : ILayoutDescriptorProvider<IFixedFieldSettingsContainer, IFixedLengthLayoutDescriptor>
    {
        public IFixedLengthLayoutDescriptor GetDescriptor<T>() => GetDescriptor(typeof(T));

        public IFixedLengthLayoutDescriptor GetDescriptor(Type t)
        {
            var container = new FieldCollection<IFixedFieldSettingsContainer>();

            var fileMappingType = t;

            var fileAttribute = fileMappingType.GetAttribute<FixedLengthFileAttribute>();
            if (fileAttribute == null)
            {
                throw new NotSupportedException(string.Format("Mapping type {0} should be marked with {1} attribute",
                    fileMappingType.Name,
                    typeof(FixedLengthFileAttribute).Name));
            }

            foreach (var ignored in fileMappingType.GetAttributes<IgnoreFixedLengthFieldAttribute>())
            {
                container.AddOrUpdate(new IgnoredFixedFieldSettings(ignored.Length) { Index = ignored.Index });
            }

            var members = fileMappingType.GetTypeDescription<FixedLengthFieldAttribute>();
            foreach (var member in members)
            {
                var settings = member.Attributes.FirstOrDefault() as IFixedFieldSettings;
                if (settings != null)
                {
                    container.AddOrUpdate(new FixedFieldSettings(member.Member, settings));
                }
            }

            var descriptor = new FixedLengthLayoutDescriptor(container, t, fileAttribute);
            return descriptor;
        }

        private sealed class FixedLengthLayoutDescriptor : LayoutDescriptorBase<IFixedFieldSettingsContainer>, IFixedLengthLayoutDescriptor
        {
            public FixedLengthLayoutDescriptor(
                IFieldCollection<IFixedFieldSettingsContainer> fieldsContainer,
                Type targetType,
                FixedLengthFileAttribute fileAttribute)
                    : base(fieldsContainer, targetType)
            {
                HasHeader = fileAttribute.HasHeader;
            }
        }
    }
}