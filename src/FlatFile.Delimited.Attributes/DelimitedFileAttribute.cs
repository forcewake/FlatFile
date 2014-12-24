namespace FlatFile.Delimited.Attributes
{
    using FlatFile.Core.Attributes.Base;

    public class DelimitedFileAttribute : LayoutBaseAttribute
    {
        public string Delimiter { get; set; }
        public string Quotes { get; set; }
    }
}