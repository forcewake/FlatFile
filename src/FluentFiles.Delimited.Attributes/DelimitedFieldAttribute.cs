namespace FluentFiles.Delimited.Attributes
{
    using FluentFiles.Core.Attributes.Base;

    public class DelimitedFieldAttribute : FieldSettingsBaseAttribute, IDelimitedFieldSettings
    {
        public string Name { get; set; }
        
        public DelimitedFieldAttribute(int index) : base(index)
        {
        }
    }
}
