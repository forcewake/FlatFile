namespace FluentFiles.Core.Base
{
    using System;
    using System.Linq.Expressions;
    using FluentFiles.Core.Extensions;

    /// <summary>
    /// Base class for record layouts.
    /// </summary>
    /// <typeparam name="TTarget">The type a file record maps to.</typeparam>
    /// <typeparam name="TFieldSettings">The type of individual field mapping within a layout.</typeparam>
    /// <typeparam name="TBuilder">The field builder type.</typeparam>
    /// <typeparam name="TLayout">The self-referencing type of layout.</typeparam>
    public abstract class LayoutBase<TTarget, TFieldSettings, TBuilder, TLayout> : LayoutDescriptorBase<TFieldSettings>, ILayout<TTarget, TFieldSettings, TBuilder, TLayout>
        where TFieldSettings : class, IFieldSettings
        where TBuilder : IFieldSettingsBuilder<TBuilder, TFieldSettings> 
        where TLayout : ILayout<TTarget, TFieldSettings, TBuilder, TLayout>
    {
        private readonly IFieldSettingsBuilderFactory<TBuilder, TFieldSettings> _fieldBuilderFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="LayoutBase{TTarget, TFieldSettings, TBuilder, TLayout}"/>.
        /// </summary>
        /// <param name="fieldBuilderFactory">Creates field builders.</param>
        /// <param name="fieldCollection">Stores field mappings.</param>
        protected LayoutBase(
            IFieldSettingsBuilderFactory<TBuilder, TFieldSettings> fieldBuilderFactory,
            IFieldCollection<TFieldSettings> fieldCollection)
                : base(fieldCollection)
        {
            _fieldBuilderFactory = fieldBuilderFactory;
            InstanceFactory = ReflectionHelper.CreateConstructor(TargetType);
        }

        protected virtual void ProcessProperty<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TBuilder> configure)
        {
            var property = expression.GetPropertyInfo();
            var builder = _fieldBuilderFactory.CreateBuilder<TTarget, TProperty>(property);

            configure?.Invoke(builder);

            var fieldSettings = builder.Build();

            FieldCollection.AddOrUpdate(fieldSettings);
        }

        /// <summary>
        /// The type a file record maps to.
        /// </summary>
        public override Type TargetType { get; } = typeof(TTarget);

        /// <summary>
        /// Creates instances of <see cref="TargetType"/>.
        /// </summary>
        public override Func<object> InstanceFactory { get; }

        /// <summary>
        /// Configures a mapping from a record field to a member of a type.
        /// </summary>
        /// <typeparam name="TProperty">The type of the member a field maps to.</typeparam>
        /// <param name="expression">An expression selecting the member to map to.</param>
        /// <param name="configure">An action that performs configuration of a field mapping.</param>
        public abstract TLayout WithMember<TProperty>(Expression<Func<TTarget, TProperty>> expression, Action<TBuilder> configure = null);

        /// <summary>
        /// Indicates that a record layout contains a header.
        /// </summary>
        /// <returns></returns>
        public abstract TLayout WithHeader();
    }
}