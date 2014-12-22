namespace FlatFile.Delimited
{
    using FlatFile.Core;
    using FlatFile.Delimited.Implementation;

    public interface IDelimitedLayout<TTarget, out TLayout> :
        ILayout<TTarget, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor, TLayout>
        where TLayout : IDelimitedLayout<TTarget, TLayout>
    {
        string Delimiter { get; }
        string Quotes { get; }

        IDelimitedLayout<TTarget, TLayout> WithQuote(string quote);
        IDelimitedLayout<TTarget, TLayout> WithDelimiter(string delimiter);
    }

    public interface IDelimitedLayout<TTarget> :
        IDelimitedLayout<TTarget, IDelimitedLayout<TTarget>>
    {
    }
}