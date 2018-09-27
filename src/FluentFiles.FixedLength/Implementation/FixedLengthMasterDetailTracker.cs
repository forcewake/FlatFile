using FluentFiles.Core.Base;

namespace FluentFiles.FixedLength.Implementation
{
    /// <summary>
    /// Uses records that implement <see cref="IMasterRecord"/> and <see cref="IDetailRecord"/> to handle
    /// master-detail record relationships.
    /// </summary>
    public class FixedLengthMasterDetailTracker : MasterDetailTrackerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthMasterDetailTracker"/> class.
        /// </summary>
        public FixedLengthMasterDetailTracker()
            : base(entry => entry is IMasterRecord,
                   entry => entry is IDetailRecord,
                   (master, detail) => ((IMasterRecord)master).DetailRecords.Add((IDetailRecord)detail))
        {
        }
    }
}