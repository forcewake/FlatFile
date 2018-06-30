namespace FlatFile.Delimited.Implementation
{
    public class DelimitedLineBuilderFactory : IDelimitedLineBuilderFactory
    {
        public IDelimitedLineBuilder GetBuilder(IDelimitedLayoutDescriptor descriptor)
        {
            return new DelimitedLineBuilder(descriptor);
        }
    }
}