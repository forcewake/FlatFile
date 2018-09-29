using System;

namespace FluentFiles.FixedLength.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class IgnoreFixedLengthFieldAttribute : Attribute
    {
        public int Index { get; }
        public int Length { get; }

        public IgnoreFixedLengthFieldAttribute(int index, int length)
        {
            Index = index;
            Length = length;
        }
    }
}