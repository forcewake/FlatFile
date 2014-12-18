namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLineBuilder<in TEntry> : ILineBulder<TEntry>
    {
    }
}