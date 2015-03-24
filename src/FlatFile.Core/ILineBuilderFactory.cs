namespace FlatFile.Core
{
    using FlatFile.Core.Base;

    public interface ILineBuilderFactory<out TBuilder, in TLayout, TFieldSettings>
        where TFieldSettings : IFieldSettings   
        where TLayout : ILayoutDescriptor<TFieldSettings>
        where TBuilder : ILineBulder
    {
        TBuilder GetBuilder(TLayout layout);
    }
}