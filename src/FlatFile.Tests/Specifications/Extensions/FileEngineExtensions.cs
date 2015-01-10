namespace FlatFile.Tests.Specifications.Defenitions.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using FlatFile.Core;

    public static class FileEngineExtensions
    {
        public static string WriteToString<T>(this IFlatFileEngine<T> engine, IEnumerable<T> source)
            where T : class, new()
        {
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                engine.Write(stream, source);
                stream.Position = 0;
                string fileContent = reader.ReadToEnd();
                return fileContent;
            }
        }
    }
}