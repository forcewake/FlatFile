namespace FluentFiles.Core.Attributes.Infrastructure
{
    /// <summary>
    /// Creates <see cref="ILayoutDescriptor{TFieldSettings}"/>s.
    /// </summary>
    /// <typeparam name="TFieldSettings">The type of field mapping in the produced layout descriptor.</typeparam>
    /// <typeparam name="TLayoutDescriptor">The type of layout descriptor.</typeparam>
    public interface ILayoutDescriptorProvider<TFieldSettings, out TLayoutDescriptor>
        where TFieldSettings : IFieldSettings
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
    {
        /// <summary>
        /// Gets an <see cref="ILayoutDescriptor{TFieldSettings}"/> for a type.
        /// </summary>
        /// <typeparam name="T">The target type a layout's fields map to.</typeparam>
        /// <returns>A new layout descriptor.</returns>
        TLayoutDescriptor GetDescriptor<T>();
    }
}
