using System;

namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedFieldSettingsConstructor :
        IFieldSettingsConstructor<IFixedFieldSettingsConstructor>
    {
        int Length { get; }
        char PaddingChar { get; }
        bool PadLeft { get; }

        IFixedFieldSettingsConstructor WithLength(int length);

        IFixedFieldSettingsConstructor WithLeftPadding(char paddingChar);

        IFixedFieldSettingsConstructor WithRightPadding(char paddingChar);

        IFixedFieldSettingsConstructor TruncateFieldContentIfExceedLength();

        IFixedFieldSettingsConstructor WithStringNormalizer(Func<string, string> stringNormalizer);
    }
}