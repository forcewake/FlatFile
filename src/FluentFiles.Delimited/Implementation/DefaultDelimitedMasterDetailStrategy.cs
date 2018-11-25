using FluentFiles.Core.Base;

namespace FluentFiles.Delimited.Implementation
{
    /// <summary>
    /// Uses records that implement <see cref="IMasterRecord"/> and <see cref="IDetailRecord"/> to handle
    /// master-detail record relationships.
    /// </summary>
    public class DefaultDelimitedMasterDetailStrategy : DelegatingMasterDetailStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultDelimitedMasterDetailStrategy"/>.
        /// </summary>
        public DefaultDelimitedMasterDetailStrategy()
            : base(entry => entry is IMasterRecord,
                   entry => entry is IDetailRecord,
                   (master, detail) => ((IMasterRecord)master).DetailRecords.Add((IDetailRecord)detail))
        {
        }
    }
}