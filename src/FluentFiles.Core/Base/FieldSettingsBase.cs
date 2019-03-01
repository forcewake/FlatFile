namespace FluentFiles.Core.Base
{
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;
    using System;
    using System.Reflection;

    internal abstract class FieldSettingsBase : IFieldSettingsContainer
    {
        private IFieldValueConverter _converter;
        private Func<object, object> _getValue;
        private Action<object, object> _setValue;

        protected readonly IFieldValueConverter DefaultConverter;

        protected FieldSettingsBase(MemberInfo member)
        {
            Type = member.MemberType();
            Member = member;
            DefaultConverter = Type.GetConverter();
            UniqueKey = $"[{member.DeclaringType.AssemblyQualifiedName}]:{member.Name}";
            _getValue = ReflectionHelper.CreateMemberGetter(member);
            _setValue = ReflectionHelper.CreateMemberSetter(member);
        }

        protected FieldSettingsBase(MemberInfo member, IFieldSettings settings)
            : this(member)
        {
            Index = settings.Index;
            IsNullable = settings.IsNullable;
            NullValue = settings.NullValue;
            Converter = settings.Converter;
        }

        public string UniqueKey { get; }
        public int? Index { get; set; }
        public bool IsNullable { get; set; }
        public string NullValue { get; set; }

        public IFieldValueConverter Converter
        {
            get => _converter ?? DefaultConverter;
            set => _converter = value;
        }

        public MemberInfo Member { get; }

        public Type Type { get; }

        public object GetValueOf(object instance) => _getValue(instance);

        public void SetValueOf(object instance, object value) => _setValue(instance, value);
    }
}