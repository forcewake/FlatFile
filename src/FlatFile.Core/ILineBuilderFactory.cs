namespace FlatFile.Core
{
    using FlatFile.Core.Base;

    public interface ILineBuilderFactory<TEntity, out TBuilder, in TLayout, TFieldSettings>
        where TFieldSettings : IFieldSettings   
        where TLayout : ILayoutDescriptor<TFieldSettings>
        where TBuilder : ILineBulder<TEntity>
    {
        TBuilder GetBuilder(TLayout layout);
    }
}