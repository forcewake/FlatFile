using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlatFile.Core;
using FlatFile.Core.Base;
using FlatFile.Core.Exceptions;

namespace FlatFile.FixedLength.Implementation
{
    public class FixedLengthFileMultiEngine : FlatFileEngine<IFixedFieldSettingsContainer, ILayoutDescriptor<IFixedFieldSettingsContainer>>, IFlatFileMultiEngine
    {
        readonly Func<string, Exception, bool> handleEntryReadError;
        readonly List<ILayoutDescriptor<IFixedFieldSettingsContainer>> layoutDescriptors;
        readonly IFixedLengthLineBuilderFactory lineBuilderFactory;
        readonly IFixedLengthLineParserFactory lineParserFactory;
        readonly Func<string, Type> typeSelectorFunc;
        readonly Dictionary<Type, ArrayList> results; 

        internal FixedLengthFileMultiEngine(
            IEnumerable<ILayoutDescriptor<IFixedFieldSettingsContainer>> layoutDescriptors,
            Func<string, Type> typeSelectorFunc,
            IFixedLengthLineBuilderFactory lineBuilderFactory,
            IFixedLengthLineParserFactory lineParserFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
        {
            if (typeSelectorFunc == null) throw new ArgumentNullException("typeSelectorFunc");
            this.layoutDescriptors = layoutDescriptors.ToList();

            foreach (var descriptor in layoutDescriptors)
            {
                results[descriptor.TargetType] = new ArrayList();
            }
            this.typeSelectorFunc = typeSelectorFunc;
            this.lineBuilderFactory = lineBuilderFactory;
            this.lineParserFactory = lineParserFactory;
            this.handleEntryReadError = handleEntryReadError;
        }

        protected override ILineBulder LineBuilder { get { throw new NotImplementedException(); } }

        protected override ILineParser LineParser { get { throw new NotImplementedException(); } }

        protected override ILayoutDescriptor<IFixedFieldSettingsContainer> LayoutDescriptor { get { throw new NotImplementedException(); } }

        public IEnumerable<TType> GetResults<TType>() where TType : Type
        {
            return !results.ContainsKey(typeof (TType)) ? null : results[typeof(TType)].Cast<TType>();
        }

        protected override bool TryParseLine<TEntity>(string line, int lineNumber, out TEntity entity)
        {
            entity = new TEntity();
            var lineParser = lineParserFactory.GetParser(layoutDescriptors.FirstOrDefault(l => l.TargetType == typeof (TEntity)));
            lineParser.ParseLine(line, entity);

            return true;
        }

        public void Read(Stream stream)
        {
            var reader = new StreamReader(stream);
            string line;
            var lineNumber = 0;


            // Todo: Can't support this in a per layout manner, it has to be for the file as a whole
            //if (LayoutDescriptor.HasHeader)
            //{
            //    ProcessHeader(reader);
            //}

            while ((line = reader.ReadLine()) != null)
            {
                var ignoreEntry = false;

                // Use selector func to find type for this line, and by effect, its layout
                var type = typeSelectorFunc(line);
                var entry = Activator.CreateInstance(type);

                try
                {
                    if (!TryParseLine(line, lineNumber++, out entry))
                    {
                        throw new ParseLineException("Impossible to parse line", line, lineNumber);
                    }
                }
                catch (Exception ex)
                {
                    if (handleEntryReadError == null)
                    {
                        throw;
                    }

                    if (!handleEntryReadError(line, ex))
                    {
                        throw;
                    }

                    ignoreEntry = true;
                }

                if (ignoreEntry) continue;

                results[type].Add(entry);
            }
        }
    }
}