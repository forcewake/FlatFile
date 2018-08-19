using FluentFiles.Core.Base;

namespace FluentFiles.Delimited.Implementation
{
    /// <summary>
    /// Uses records that implement <see cref="IMasterRecord"/> and <see cref="IDetailRecord"/> to handle
    /// master-detail record relationships.
    /// </summary>
    public class DelimitedMasterDetailTracker : MasterDetailTrackerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterDetailTracker"/> class.
        /// </summary>
        public DelimitedMasterDetailTracker()
            : base(entry => entry is IMasterRecord,
                   entry => entry is IDetailRecord,
                   (master, detail) => ((IMasterRecord)master).DetailRecords.Add((IDetailRecord)detail))
        {
        }
    }
}