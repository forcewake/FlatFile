namespace FluentFiles.FixedLength
{
    using System;
    using FluentFiles.Core;

    public interface IFixedFieldSettings : IFieldSettings
    {
        int Length { get; }
        bool PadLeft { get; }
        char PaddingChar { get; }
        Func<char, int, bool> SkipWhile { get; }
        Func<char, int, bool> TakeUntil { get; }
        bool TruncateIfExceedFieldLength { get; }
        Func<string, string> StringNormalizer { get; }
    }

    public interface IFixedFieldSettingsContainer : IFixedFieldSettings, IFieldSettingsContainer
    {
    }
}