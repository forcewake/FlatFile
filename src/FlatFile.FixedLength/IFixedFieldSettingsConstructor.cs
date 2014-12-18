namespace FlatFile.FixedLength
{
    using FlatFile.Core;
    using FlatFile.FixedLength.Implementation;

    public interface IFixedFieldSettingsConstructor :
        IFieldSettingsConstructor<FixedFieldSettings, IFixedFieldSettingsConstructor>
    {
        int Lenght { get; }
        char PaddingChar { get; }
        bool PadLeft { get; }

        IFixedFieldSettingsConstructor WithLenght(int lenght);

        IFixedFieldSettingsConstructor WithLeftPadding(char paddingChar);

        IFixedFieldSettingsConstructor WithRightPadding(char paddingChar);
    }
}