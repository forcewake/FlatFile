using System;
using FlatFile.Core.Base;

namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    /// <summary>
    /// Interface IFixedLengthLineParserFactory
    /// </summary>
    public interface IFixedLengthLineParserFactory :
        ILineParserFactory<IFixedLengthLineParser, ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>
    {
        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetType" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetType">The target record type.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void RegisterLineParser<TParser>(Type targetType) where TParser : IFixedLengthLineParser;

        /// <summary>
        /// Registers the line parser <typeparamref name="TParser" /> for lines matching <paramref name="targetLayout" />.
        /// </summary>
        /// <typeparam name="TParser">The type of the t parser.</typeparam>
        /// <param name="targetLayout">The target layout.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void RegisterLineParser<TParser>(ILayoutDescriptor<IFieldSettings> targetLayout) where TParser : IFixedLengthLineParser;
    }
}