namespace FluentFiles.Core
{
    using FluentFiles.Core.Base;

    public interface ILineParserFactory<out TParser, in TLayoutDescriptor, TFieldSettings>
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
        where TParser : ILineParser
    {
        TParser GetParser(TLayoutDescriptor descriptor);
    }
}