namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedFieldSettingsConstructor :
        IFieldSettingsConstructor<IFixedFieldSettingsConstructor>
    {
        int Lenght { get; }
        char PaddingChar { get; }
        bool PadLeft { get; }

        IFixedFieldSettingsConstructor WithLenght(int lenght);

        IFixedFieldSettingsConstructor WithLeftPadding(char paddingChar);

        IFixedFieldSettingsConstructor WithRightPadding(char paddingChar);
    }
}