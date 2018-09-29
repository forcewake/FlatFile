namespace FluentFiles.FixedLength.Implementation
{
    public class FixedLengthLineBuilderFactory : IFixedLengthLineBuilderFactory
    {
        public IFixedLengthLineBuilder GetBuilder(IFixedLengthLayoutDescriptor descriptor)
        {
            return new FixedLengthLineBuilder(descriptor);
        }
    }
}