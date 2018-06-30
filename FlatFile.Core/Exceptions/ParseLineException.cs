namespace FlatFile.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ParseLineException : Exception
    {
        public ParseLineException(string message, Exception innerException, string line, int lineNumber)
            : base(message, innerException)
        {
            Line = line;
            LineNumber = lineNumber;
        }

        public ParseLineException(string message, string line, int lineNumber)
            : base(message)
        {
            Line = line;
            LineNumber = lineNumber;
        }

        public ParseLineException(string line, int lineNumber)
        {
            Line = line;
            LineNumber = lineNumber;
        }

        public int LineNumber { get; private set; }
        public string Line { get; private set; }

        protected ParseLineException(SerializationInfo info, StreamingContext context, string line, int lineNumber)
            : base(info, context)
        {
            Line = line;
            LineNumber = lineNumber;
        }
    }
}
