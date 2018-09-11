namespace FluentFiles.Tests.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;
    using FluentFiles.Tests.Base.Entities;
    using FluentAssertions;
    using Xunit;

    public abstract class IntegrationTests<TFieldSettings, TConstructor, TLayout>
        where TLayout : ILayout<TestObject, TFieldSettings, TConstructor, TLayout>
        where TFieldSettings : IFieldSettingsContainer
        where TConstructor : IFieldSettingsBuilder<TConstructor, TFieldSettings> 
    {
        protected abstract TLayout Layout { get; }

        protected IList<TestObject> Objects { get; set; }

        protected abstract IFlatFileEngine Engine { get; }

        public abstract string TestSource { get; }

        protected IntegrationTests()
        {
            Objects = new List<TestObject>();

            for (int i = 1; i <= 10; i++)
            {
                Objects.Add(new TestObject
                {
                    Id = i,
                    Description = "Description " + i,
                    NullableInt = i%5 == 0 ? null : (int?) 3
                });
            }
        }

        [Fact]
        public virtual void CountOfTheObjectsAfterWriteReadShouldBeTheSame()
        {
            InvokeWriteTest((engine, stream) =>
            {
                var objectsAfterRead = engine.Read<TestObject>(stream).ToArray();

                objectsAfterRead.Should().HaveCount(Objects.Count);

            });
        }

        [Fact]
        public virtual void AllDeclaredPropertiesOfTheObjectsAfterWriteReadShouldBeTheSame()
        {
            InvokeWriteTest((engine, stream) =>
            {
                var objectsAfterRead = engine.Read<TestObject>(stream).ToList();

                objectsAfterRead.Should().BeEquivalentTo(Objects, options => options.IncludingAllDeclaredProperties());

            });
        }

        [Fact]
        public void AllDeclaredPropertiesOfTheObjectsAfterReadFromSourceShouldBeTheSame()
        {
            InvokeReadbasedTest((engine, stream) =>
            {
                var objectsAfterRead = engine.Read<TestObject>(stream).ToList();

                objectsAfterRead.Should().BeEquivalentTo(Objects, options => options.IncludingAllDeclaredProperties());

            }, TestSource);
        }

        protected virtual void InvokeWriteTest(Action<IFlatFileEngine, MemoryStream> action)
        {
            using (var memory = new MemoryStream())
            {
                Engine.Write(memory, Objects);

                memory.Seek(0, SeekOrigin.Begin);

                action(Engine, memory);
            }
        }

        protected virtual void InvokeReadbasedTest(Action<IFlatFileEngine, MemoryStream> action,
            string textSource)
        {
            using (var memory = new MemoryStream(Encoding.UTF8.GetBytes(textSource)))
            {
                action(Engine, memory);
            }
        }
    }
}