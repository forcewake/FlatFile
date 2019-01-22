using System;

namespace FluentFiles.Core.Base
{
    /// <summary>
    /// Uses records that implement <see cref="IMasterRecord"/> and <see cref="IDetailRecord"/> to handle
    /// master-detail record relationships.
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