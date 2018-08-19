namespace FluentFiles.Delimited.Attributes
{
    using FluentFiles.Core.Attributes.Base;

    public class DelimitedFileAttribute : LayoutBaseAttribute
    {
        public string Delimiter { get; set; }
        public string Quotes { get; set; }
    }
}