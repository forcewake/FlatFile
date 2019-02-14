namespace FluentFiles.Core
{
    /// <summary>
    /// Interface for an object that can build another object.
    /// </summary>
    /// <typeparam name="TOutput">The type of object that can be built.</typeparam>
    public interface IBuildable<out TOutput>
    {
        /// <summary>
        /// Finalizes a builder and produces its output.
        /// </summary>
        TOutput Build();
    }
}