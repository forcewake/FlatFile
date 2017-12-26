using System.Collections.Generic;
using System.IO;

namespace FlatFile.Core
{
    /// <summary>
    /// Interface IFlatFileMultiEngine
    /// </summary>
    public interface IFlatFileMultiEngine : IFlatFileEngine
    {
        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void Read(Stream stream);
        /// <summary>
        /// Reads the specified stream reader.
        /// </summary>
        /// <param name="streamReader">The stream reader.</param>
        void Read(StreamReader streamReader);
        /// <summary>
        /// Gets any records of type <typeparamref name="T"/> read by <see cref="Read"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetRecords<T>() where T : class, new();
        /// <summary>
        /// Gets or sets a value indicating whether this instance has a file header.
        /// </summary>
        /// <value><c>true</c> if this instance has a file header; otherwise, <c>false</c>.</value>
        bool HasHeader { get; set; }
    }
}