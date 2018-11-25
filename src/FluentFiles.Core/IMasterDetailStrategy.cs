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
        /// <param name="isDetailRecord">Whether the record is a master or detail record.</param>
        void HandleMasterDetail(object record, out bool isDetailRecord);
    }
}