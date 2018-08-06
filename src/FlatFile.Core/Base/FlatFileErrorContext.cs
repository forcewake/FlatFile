namespace FlatFile.Core.Base
{
    using System;

    /// <summary>
    /// Provides information about a file parsing error.
    /// </summary>
    public struct FlatFileErrorContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FlatFileErrorContext"/>.
        /// </summary>
        /// <param name="line">The content of the line on which the error occurred.</param>
        /// <param name="lineNumber">The line numer at which the error occurred.</param>
        /// <param name="exception">The error that occurred.</param>
        public FlatFileErrorContext(string line, int lineNumber, Exception exception)
        {
            Line = line;
            LineNumber = lineNumber;
            Exception = exception;
        }

        /// <summary>
        /// The content of the line on which the error occurred.
        /// </summary>
        public string Line { get; private set; }

        /// <summary>
        /// The line numer at which the error occurred.
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// The error that occurred.
        /// </summary>
        public Exception Exception { get; private set; }
    }
}