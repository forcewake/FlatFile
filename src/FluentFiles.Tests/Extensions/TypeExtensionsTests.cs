using FluentFiles.Core.Extensions;
using System;
using Xunit;

namespace FluentFiles.Tests.Extensions
{
    public class TypeExtensionsTests
    {
        [Theory]
        [MemberData(nameof(DefaultValueData))]
        public void Test_GetDefaultValue(Type type, object expected)
        {
            // Act.
            var actual = type.GetDefaultValue();

            // Assert.
            Assert.Equal(expected, actual);
        }

        public static TheoryData<Type, object> DefaultValueData()
        {
            return new TheoryData<Type, object>
            {
                { typeof(object),   null },
                { typeof(string),   null },
                { typeof(int),      0 },
                { typeof(byte),     (byte)0 },
                { typeof(double),   0.0d },
                { typeof(decimal),  0.0m },
                { typeof(int?),     null },
                { typeof(double?),  null },
                { typeof(decimal?), null }
            };
        }
    }
}
