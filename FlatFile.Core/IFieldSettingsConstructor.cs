namespace FlatFile.Core
{
    using FlatFile.Core.Base;

    public interface IFieldSettingsConstructor<out TConstructor> : IFieldSettingsContainer
        where TConstructor : IFieldSettingsConstructor<TConstructor>
    {
        TConstructor AllowNull(string nullValue);
        TConstructor WithTypeConverter<TConverter>() where TConverter : ITypeConverter;
    }
}