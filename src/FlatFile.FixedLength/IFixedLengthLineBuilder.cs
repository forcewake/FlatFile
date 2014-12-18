namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLengthLineBuilder<in TEntity> : ILineBulder<TEntity>
    {
    }
}