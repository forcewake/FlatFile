namespace FluentFiles.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A file engine capable of handling files with multiple types of records.
    /// </summary>
    public interface IFlatFileMultiEngine : IFlatFileEngine
    {
        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="cancellationToken">Cancels reading a file.</param>
        Task ReadAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Reads the specified text reader.
        /// </summary>
        /// <param name="reader">The text reader.</param>
        /// <param name="cancellationToken">Cancels reading a file.</param>
        Task ReadAsync(TextReader reader, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets any records of type <typeparamref name="T"/> read by <see cref="ReadAsync(Stream, CancellationToken)"/> or <see cref="ReadAsync(TextReader, CancellationToken)"/>.
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