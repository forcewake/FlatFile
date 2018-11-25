using FluentFiles.Core.Base;

namespace FluentFiles.FixedLength.Implementation
{
    /// <summary>
    /// Uses records that implement <see cref="IMasterRecord"/> and <see cref="IDetailRecord"/> to handle
    /// master-detail record relationships.
    /// </summary>
    public class DefaultFixedLengthMasterDetailStrategy : DelegatingMasterDetailStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultFixedLengthMasterDetailStrategy"/>.
        /// </summary>
        public DefaultFixedLengthMasterDetailStrategy()
            : base(entry => entry is IMasterRecord,
                   entry => entry is IDetailRecord,
                   (master, detail) => ((IMasterRecord)master).DetailRecords.Add((IDetailRecord)detail))
        {
        }
    }
}