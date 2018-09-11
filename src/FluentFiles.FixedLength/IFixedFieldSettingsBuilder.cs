using System;

namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    public interface IFixedFieldSettingsBuilder : IFieldSettingsBuilder<IFixedFieldSettingsBuilder, IFixedFieldSettingsContainer>
    {
        IFixedFieldSettingsBuilder WithLength(int length);

        IFixedFieldSettingsBuilder WithLeftPadding(char paddingChar);

        IFixedFieldSettingsBuilder WithRightPadding(char paddingChar);

        IFixedFieldSettingsBuilder TruncateFieldContentIfExceedLength();

        IFixedFieldSettingsBuilder WithStringNormalizer(Func<string, string> stringNormalizer);
    }
}