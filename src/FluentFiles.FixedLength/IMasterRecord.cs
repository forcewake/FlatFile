using System.Collections.Generic;
using FluentFiles.Core;

namespace FluentFiles.FixedLength
{
    /// <summary>
    /// Interface IMasterRecord
    /// </summary>
    /// <remarks>
    /// Used to decorate a record as a master type in a master/detail relationship.
    /// </remarks>
    public interface IMasterRecord
    {
        /// <summary>
        /// Gets the detail records.
        /// </summary>
        /// <value>The detail records.</value>
        /// <remarks>
        /// This list will be populated with related detail records when parsing a fixed length file with the <see cref="IFlatFileMultiEngine"/>
        /// </remarks>
        IList<IDetailRecord> DetailRecords { get; }
    }
}