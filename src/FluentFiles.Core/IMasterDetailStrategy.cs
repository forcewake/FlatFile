namespace FluentFiles.Core
{
    /// <summary>
    /// Determines how master-detail record relationships are handled.
    /// </summary>
    public interface IMasterDetailStrategy
    {
        /// <summary>
        /// Handles a record and determines whether it is a master or detail record.
        /// </summary>
        /// <param name="record">The record to handle.</param>
        /// <returns>True if the record is a detail and false if it is a master record.</returns>
        bool Handle(object record);
    }
}