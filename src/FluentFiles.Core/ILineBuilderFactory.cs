namespace FluentFiles.Core
{
    /// <summary>
    /// Interface for an object that creates line builders.
    /// </summary>
    /// <typeparam name="TBuilder">The type of builder to create.</typeparam>
    /// <typeparam name="TLayout">The type of layout the builder is for.</typeparam>
    /// <typeparam name="TFieldSettings">The type of field mapping configuration used in the layout.</typeparam>
    public interface ILineBuilderFactory<out TBuilder, in TLayout, TFieldSettings>
        where TFieldSettings : IFieldSettings   
        where TLayout : ILayoutDescriptor<TFieldSettings>
        where TBuilder : ILineBuilder
    {
        /// <summary>
        /// Gets a line builder.
        /// </summary>
        /// <param name="layout">The layout a builder is for.</param>
        /// <returns>A new line builder.</returns>
        TBuilder GetBuilder(TLayout layout);
    }
}