namespace FluentFiles.Core.Base
{
    using System;

    /// <summary>
    /// An <see cref="IMasterDetailStrategy"/> that delegates its implementation checks to
    /// functions provided at creation time.
    /// </summary>
    public class DelegatingMasterDetailStrategy : IMasterDetailStrategy
    {
        /// <summary>
        /// Determines whether a record is a master record.
        /// </summary>
        private readonly Func<object, bool> _checkIsMasterRecord;

        /// <summary>
        /// Determines whether a record is a detail record.
        /// </summary>
        private readonly Func<object, bool> _checkIsDetailRecord;

        /// <summary>
        /// Handles confirmed detail records.
        /// </summary>
        private readonly Action<object, object> _handleDetailRecord;

        /// <summary>
        /// The last record parsed that was determined to be a master record.
        /// </summary>
        private object _lastMasterRecord;

        /// <summary>
        /// Initializes a new instance of <see cref="DelegatingMasterDetailStrategy"/>.
        /// </summary>
        /// <param name="checkIsMasterRecord">Determines whether a record is a master record.</param>
        /// <param name="checkIsDetailRecord">Determines whether a record is a detail record.</param>
        /// <param name="handleDetailRecord">Handles confirmed detail records.</param>
        public DelegatingMasterDetailStrategy(
            Func<object, bool> checkIsMasterRecord,
            Func<object, bool> checkIsDetailRecord,
            Action<object, object> handleDetailRecord)
        {
            _checkIsMasterRecord = checkIsMasterRecord ?? throw new ArgumentNullException(nameof(checkIsMasterRecord));
            _checkIsDetailRecord = checkIsDetailRecord ?? throw new ArgumentNullException(nameof(checkIsDetailRecord));
            _handleDetailRecord = handleDetailRecord ?? throw new ArgumentNullException(nameof(handleDetailRecord));
        }

        /// <summary>
        /// Handles a record and determines whether it is a master or detail record.
        /// </summary>
        /// <param name="record">The record to handle.</param>
        /// <param name="isDetailRecord">Whether the record is a master or detail record.</param>
        public void HandleMasterDetail(object record, out bool isDetailRecord)
        {
            isDetailRecord = false;

            if (_checkIsMasterRecord(record))
            {
                // Found new master record
                _lastMasterRecord = record;
                return;
            }

            // Record is standalone or unassociated detail record
            if (_lastMasterRecord == null) return;

            if (!_checkIsDetailRecord(record))
            {
                // Record is standalone, reset master
                _lastMasterRecord = null;
                return;
            }

            // Add detail record and indicate that it should not be added to the results dictionary
            _handleDetailRecord(_lastMasterRecord, record);
            isDetailRecord = true;
        }
    }
}