namespace FluentFiles.Core
{
    public interface IBuildable<out TOutput>
    {
        /// <summary>
        /// Finalizes a builder.
        /// </summary>
        TOutput Build();
    }
}