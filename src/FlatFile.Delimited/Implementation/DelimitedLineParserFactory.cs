namespace FlatFile.Delimited.Implementation
{
    public class DelimitedLineParserFactory : IDelimitedLineParserFactory
    {
        public IDelimitedLineParser GetParser(IDelimitedLayoutDescriptor descriptor)
        {
            return new DelimitedLineParser(descriptor);
        }
    }
}