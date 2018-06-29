namespace FlatFile.Tests.Specifications.Entities
{
    using FlatFile.FixedLength;

    public class FixedLengthTypeMapping
    {
        public string Name { get; set; }
        public int Length { get; set; }

        public string PaddingChar { get; set; }

        public char PaddingCharElement
        {
            get
            {
                if (PaddingChar == "<space>")
                {
                    PaddingChar = " ";
                }

                char element;
                
                if (!char.TryParse(PaddingChar, out element))
                {
                    element = '\0';
                }

                return element;
            }
        }

        public Padding Padding { get; set; }

        public string NullValue { get; set; }
    }
}