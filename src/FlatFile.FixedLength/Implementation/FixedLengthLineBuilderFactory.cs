namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core;

    public class FixedLengthLineBuilderFactory<TEntity> : IFixedLengthLineBuilderFactory<TEntity>
    {
        public IFixedLengthLineBuilder<TEntity> GetBuilder(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
        {
            return new FixedLengthLineBuilder<TEntity>(descriptor);
        }
    }
}