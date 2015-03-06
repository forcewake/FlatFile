namespace FlatFile.FixedLength
{
    using System.Reflection;
    using FlatFile.Core.Base;

    public interface IFixedFieldSettings : IFieldSettings
    {
        int Length { get; }
        bool PadLeft { get; }
        char PaddingChar { get; }
    }

    public interface IFixedFieldSettingsContainer : IFixedFieldSettings, IFieldSettingsContainer
    {
    }

    public class FixedFieldSettings : FieldSettingsBase, IFixedFieldSettingsContainer
    {
        public FixedFieldSettings(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public FixedFieldSettings(IFixedFieldSettings settings)
            : base(settings)
        {
            Length = settings.Length;
            PadLeft = settings.PadLeft;
            PaddingChar = settings.PaddingChar;
        }

        public FixedFieldSettings(PropertyInfo propertyInfo, IFixedFieldSettings settings)
            : this(settings)
        {
            PropertyInfo = propertyInfo;
        }

        public int Length { get; set; }
        public bool PadLeft { get; set; }
        public char PaddingChar { get; set; }
    }
}