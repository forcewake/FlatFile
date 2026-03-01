using FlatFile.Core.Extensions;
using Xunit;

namespace FlatFile.Modern.Tests;

public class ReflectionHelperTests
{
    private sealed class Sample
    {
        public int Value { get; }
        public Sample() { Value = 42; }
        public Sample(int value) { Value = value; }
    }

    [Fact]
    public void CreateInstance_Cached_DefaultConstructor_Works()
    {
        var instance = (Sample)ReflectionHelper.CreateInstance(typeof(Sample), cached: true)!;
        Assert.Equal(42, instance.Value);
    }

    [Fact]
    public void CreateInstance_Cached_WithArguments_Works()
    {
        var instance = (Sample)ReflectionHelper.CreateInstance(typeof(Sample), cached: true, 7)!;
        Assert.Equal(7, instance.Value);
    }
}
