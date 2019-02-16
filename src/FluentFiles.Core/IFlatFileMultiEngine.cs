namespace FluentFiles.Core
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// A file engine capable of handling files with multiple types of records.
    /// </summary>
    public interface IFlatFileMultiEngine : IFlatFileEngine
    {
        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void Read(Stream stream);

        /// <summary>
        /// Reads the specified text reader.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        void Read(TextReader reader);

        /// <summary>
        /// Gets any records of type <typeparamref name="T"/> read by <see cref="Read(Stream)"/> or <see cref="Read(TextReader)"/>.
        /// </summary>
        /// <typeparam name="T">The type of record to retrieve.</typeparam>
        /// <returns>Any records of type <typeparamref name="T"/> that were parsed.</returns>
        IEnumerable<T> GetRecords<T>() where T : class, new();

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a file header.
        /// </summary>
        /// <value><c>true</c> if this instance has a file header; otherwise, <c>false</c>.</value>
        bool HasHeader { get; set; }
    }
}