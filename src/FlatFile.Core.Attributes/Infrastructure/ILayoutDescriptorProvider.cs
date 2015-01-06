namespace FlatFile.Core.Attributes.Infrastructure
{
    using FlatFile.Core.Base;

    public interface ILayoutDescriptorProvider<TFieldSettings, out TLayoutDescriptor>
        where TFieldSettings : FieldSettingsBase
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
    {
        TLayoutDescriptor GetDescriptor<T>();
    }
}
