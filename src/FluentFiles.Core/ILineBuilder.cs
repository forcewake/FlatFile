namespace FluentFiles.Core
{
    /// <summary>
    /// Represents an object that can convert records into lines of a file.
    /// </summary>
    public interface ILineBuilder
    {
        /// <summary>
        /// Maps an instance of <typeparamref name="TRecord"/> to a line in a file.
        /// </summary>
        /// <typeparam name="TRecord">The type to map.</typeparam>
        /// <param name="entity">The instance to map.</param>
        /// <returns>The formatted value of the data in <paramref name="entity"/>.</returns>
        string BuildLine<TRecord>(TRecord entity);
    }
}