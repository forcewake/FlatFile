namespace FlatFile.Core
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Interface IFlatFileEngine
    /// </summary>
    public interface IFlatFileEngine
    {
        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        IEnumerable<TEntity> Read<TEntity>(Stream stream) where TEntity : class, new();

        /// <summary>
        /// Reads from the specified text reader.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="stream">The text reader.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        IEnumerable<TEntity> Read<TEntity>(TextReader reader) where TEntity : class, new();

        /// <summary>
        /// Writes to the specified stream.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="entries">The entries.</param>
        void Write<TEntity>(Stream stream, IEnumerable<TEntity> entries) where TEntity : class, new();

        /// <summary>
        /// Writes to the specified text writer.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="entries">The entries.</param>
        void Write<TEntity>(TextWriter writer, IEnumerable<TEntity> entries) where TEntity : class, new();
    }
}