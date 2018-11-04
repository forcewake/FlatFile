namespace FluentFiles.Core.Base
{
    /// <summary>
    /// A function used to handle errors encountered during reading and parsing of a file.
    /// To ignore and continue, true should be returned. To halt processing and throw
    /// and exception, return false.
    /// </summary>
    /// <param name="context">Contextual information about the error.</param>
    /// <returns>True to continue parsing, false to halt and throw an error.</returns>
    public delegate bool FileReadErrorHandler(FlatFileErrorContext context);
}