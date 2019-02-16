namespace FluentFiles.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown when an eror occurs when parsing a line.
    /// </summary>
    [Serializable]
    public class ParseLineException : Exception
    {
        /// <summary>
        /// Initializes a new <see cref="ParseLineException"/>.
        /// </summary>
        public ParseLineException() { }

        /// <summary>
        /// Initializes a new <see cref="ParseLineException"/>.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ParseLineException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="ParseLineException"/>.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The exception that caused this one.</param>
        public ParseLineException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="ParseLineException"/>.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The exception that caused this one.</param>
        /// <param name="line">The line that failed to parse.</param>
        /// <param name="lineNumber">The number of the line that failed to parse.</param>
        public ParseLineException(string message, Exception innerException, string line, int lineNumber)
            : this(message, innerException)
        {
            Line = line;
            LineNumber = lineNumber;
        }

        /// <summary>
        /// Initializes a new <see cref="ParseLineException"/>.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="line">The line that failed to parse.</param>
        /// <param name="lineNumber">The number of the line that failed to parse.</param>
        public ParseLineException(string message, string line, int lineNumber)
            : this(message)
        {
            Line = line;
            LineNumber = lineNumber;
        }

        /// <summary>
        /// Initializes a new <see cref="ParseLineException"/>.
        /// </summary>
        /// <param name="line">The line that failed to parse.</param>
        /// <param name="lineNumber">The number of the line that failed to parse.</param>
        public ParseLineException(string line, int lineNumber)
        {
            Line = line;
            LineNumber = lineNumber;
        }

        /// <summary>
        /// The line that failed to parse.
        /// </summary>
        public string Line { get; }

        /// <summary>
        /// The number of the line that failed to parse.
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        /// Initializes a new <see cref="ParseLineException"/> with serialized data.
        /// </summary>
        /// <param name="info">Contains serialized data.</param>
        /// <param name="context">Contains information about the serialization stream.</param>
        /// <param name="line">The line that failed to parse.</param>
        /// <param name="lineNumber">The number of the line that failed to parse.</param>
        protected ParseLineException(SerializationInfo info, StreamingContext context, string line, int lineNumber)
            : base(info, context)
        {
            Line = line;
            LineNumber = lineNumber;
        }
    }
}
