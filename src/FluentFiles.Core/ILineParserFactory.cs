namespace FluentFiles.Core
{
    /// <summary>
    /// Interface for an object that can parse lines of a file.
    /// </summary>
    /// <typeparam name="TParser">The type of parser to create.</typeparam>
    /// <typeparam name="TLayoutDescriptor">The type of layout a parser is for.</typeparam>
    /// <typeparam name="TFieldSettings">The type of field mapping configuration in the layout.</typeparam>
    public interface ILineParserFactory<out TParser, in TLayoutDescriptor, TFieldSettings>
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
        where TParser : ILineParser
    {
        /// <summary>
        /// Gets a parser.
        /// </summary>
        /// <param name="descriptor">The layout descriptor a builder is for.</param>
        /// <returns>A new line parser.</returns>
        TParser GetParser(TLayoutDescriptor descriptor);
    }
}