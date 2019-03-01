namespace FluentFiles.Core.Attributes.Entities
{
    using System;
    using System.Reflection;

    internal class MemberDescription
    {
        public MemberDescription(MemberInfo member, Attribute[] attributes)
        {
            Member = member;
            Attributes = attributes;
        }

        public MemberInfo Member { get; }
        public Attribute[] Attributes { get; }
    }
}
