namespace FluentFiles.Core.Base
{
    using System;

    /// <summary>
    /// Provides information about a file parsing error.
    /// </summary>
    public struct FlatFileErrorContext
    {
        private readonly string line;
        private readonly int lineNumber;
        private readonly Exception exception;

        /// <summary>
        /// Initializes a new instance of <see cref="FlatFileErrorContext"/>.
        /// </summary>
        /// <param name="line">The content of the line on which the error occurred.</param>
        /// <param name="lineNumber">The line numer at which the error occurred.</param>
        /// <param name="exception">The error that occurred.</param>
        public FlatFileErrorContext(string line, int lineNumber, Exception exception)
        {
            this.line = line;
            this.lineNumber = lineNumber;
            this.exception = exception;
        }

        /// <summary>
        /// The content of the line on which the error occurred.
        /// </summary>
        public string Line
        {
            get { return line; }
        }

        /// <summary>
        /// The line numer at which the error occurred.
        /// </summary>
        public int LineNumber
        {
            get { return lineNumber; }
        }

        /// <summary>
        /// The error that occurred.
        /// </summary>
        public Exception Exception
        {
            get { return exception; }
        }
    }
}