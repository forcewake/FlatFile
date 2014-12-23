namespace FlatFile.Core
{
    using System.Collections.Generic;
    using System.IO;
    using FlatFile.Core.Base;

    public interface IFlatFileEngine<TEntity, in TLayout, TFieldSettings, TConstructor>
        where TEntity : class, new()
        where TLayout : ILayout<TEntity, TFieldSettings, TConstructor, TLayout>
        where TFieldSettings : FieldSettingsBase
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
    {
        IEnumerable<TEntity> Read(TLayout layout, Stream stream);

        void Write(TLayout layout, Stream stream, IEnumerable<TEntity> entries);
    }
}