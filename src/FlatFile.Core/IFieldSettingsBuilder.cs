namespace FlatFile.Core
{
    using FlatFile.Core.Base;

    public interface IFieldSettingsBuilder<out TFieldSettings, in TConstructor>
        where TFieldSettings : FieldSettingsBase
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
    {
        TFieldSettings BuildSettings(TConstructor constructor);
    }
}