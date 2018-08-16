namespace FlatFile.Tests.Specifications.Transforms
{
    using System.Collections.Generic;
    using System.Linq;
    using FlatFile.Tests.Base.Entities;
    using FlatFile.Tests.Specifications.Extensions;
    using TechTalk.SpecFlow;

    [Binding]
    public class TransformsBinding
    {
        [StepArgumentTransformation]
        public IEnumerable<TestObject> TestObjectsTransform(Table testObjectsTable)
        {
            return testObjectsTable
                .Rows
                .Select(r => new TestObject
                {
                    Id = int.Parse(r["Id"]),
                    Description = r["Description"],
                    NullableInt = StringExtensions.ToNullableInt32(r["NullableInt"])
                }).ToArray();
        }
    }
}