using System;

namespace FlatFile.Core.Base
{
    /// <summary>
    /// Uses records that implement <see cref="IMasterRecord"/> and <see cref="IDetailRecord"/> to handle
    /// master-detail record relationships.
    /// </summary>
    public class MasterDetailTrackerBase : IMasterDetailTracker
    {
        /// <summary>
        /// Determines whether a record is a master record.
        /// </summary>
        readonly Func<object, bool> checkIsMasterRecord;
        /// <summary>
        /// Determines whether a record is a detail record.
        /// </summary>
        readonly Func<object, bool> checkIsDetailRecord;
        /// <summary>
        /// Handles confirmed detail records.
        /// </summary>
        readonly Action<object, object> handleDetailRecord;
        /// <summary>
        /// The last record parsed that implements <see cref="IMasterRecord"/>
        /// </summary>
        object lastMasterRecord;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterDetailTracker"/> class.
        /// </summary>
        /// <param name="checkIsMasterRecord">Determines whether a record is a master record.</param>
        /// <param name="checkIsDetailRecord">Determines whether a record is a detail record.</param>
        /// <param name="handleDetailRecord">Handles confirmed detail records.</param>
        public MasterDetailTrackerBase(
            Func<object, bool> checkIsMasterRecord,
            Func<object, bool> checkIsDetailRecord,
            Action<object, object> handleDetailRecord)
        {
            this.checkIsMasterRecord = checkIsMasterRecord;
            this.checkIsDetailRecord = checkIsDetailRecord;
            this.handleDetailRecord = handleDetailRecord;
        }

        public void HandleMasterDetail(object entry, out bool isDetailRecord)
        {
            isDetailRecord = false;

            if (checkIsMasterRecord(entry))
            {
                // Found new master record
                lastMasterRecord = entry;
                return;
            }

            // Record is standalone or unassociated detail record
            if (lastMasterRecord == null) return;

            if (!checkIsDetailRecord(entry))
            {
                // Record is standalone, reset master
                lastMasterRecord = null;
                return;
            }

            // Add detail record and indicate that it should not be added to the results dictionary
            handleDetailRecord(lastMasterRecord, entry);
            isDetailRecord = true;
        }
    }
}