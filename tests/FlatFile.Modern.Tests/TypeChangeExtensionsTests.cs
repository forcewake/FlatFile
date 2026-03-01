using System;
using FlatFile.Core.Extensions;
using Xunit;

namespace FlatFile.Modern.Tests;

public class TypeChangeExtensionsTests
{
    [Fact]
    public void Convert_UsesInvariantCulture_ForDecimal()
    {
        var value = (decimal)"1234.56".Convert(typeof(decimal));
        Assert.Equal(1234.56m, value);
    }

    [Fact]
    public void Convert_HandlesNullableAndEnum()
    {
        var nullValue = (int?)"".Convert(typeof(int?));
        var day = (DayOfWeek)"friday".Convert(typeof(DayOfWeek));

        Assert.Null(nullValue);
        Assert.Equal(DayOfWeek.Friday, day);
    }
}
