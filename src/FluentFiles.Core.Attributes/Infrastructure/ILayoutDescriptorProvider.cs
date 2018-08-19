namespace FluentFiles.Core.Attributes.Infrastructure
{
    using FluentFiles.Core.Base;

    public interface ILayoutDescriptorProvider<TFieldSettings, out TLayoutDescriptor>
        where TFieldSettings : IFieldSettings
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
    {
        TLayoutDescriptor GetDescriptor<T>();
    }
}
