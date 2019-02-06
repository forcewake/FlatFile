using System;

namespace FluentFiles.Core
{
    /// <summary>
    /// Represents an object that can convert the lines of a file into record instances.
    /// </summary>
    public interface ILineParser
    {
        /// <summary>
        /// Maps a line of a file to an instance of <typeparamref name="TRecord"/>.
        /// </summary>
        /// <typeparam name="TRecord">The type to map to.</typeparam>
        /// <param name="line">The file line to parse.</param>
        /// <param name="entity">The instance to populate.</param>
        /// <returns>An instance of <typeparamref name="TRecord"/> populated with the parsed and transformed data from <paramref name="line"/>.</returns>
        TRecord ParseLine<TRecord>(ReadOnlySpan<char> line, TRecord entity) where TRecord : new();
    }
}