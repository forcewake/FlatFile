FlatFile 
========
[![Build status](https://ci.appveyor.com/api/projects/status/9uoix14g3w0rac3q?svg=true)](https://ci.appveyor.com/project/forcewake/flatfile)
[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/forcewake/flatfile/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

FlatFile is a library to work with flat files (work up-to 100 times faster then [FileHelpers](https://www.nuget.org/packages/FileHelpers/2.0.0))

### Installing FlatFile

#### Installing all packages
You should install [FlatFile with NuGet](https://www.nuget.org/packages/FlatFile):

```sh
Install-Package FlatFile
```

#### Installing FlatFile.Delimited
You should install [FlatFile.Delimited with NuGet](https://www.nuget.org/packages/FlatFile.Delimited):

```sh
Install-Package FlatFile.Delimited
```

##### Add attribute-mapping extensions
You should install [FlatFile.Delimited.Attributes with NuGet](https://www.nuget.org/packages/FlatFile.Delimited.Attributes):

```sh
Install-Package FlatFile.Delimited.Attributes
```

#### Installing FlatFile.FixedLength
You should install [FlatFile.FixedLength with NuGet](https://www.nuget.org/packages/FlatFile.FixedLength):

```sh
Install-Package FlatFile.FixedLength
```

##### Add attribute-mapping extensions
You should install [FlatFile.FixedLength.Attributes with NuGet](https://www.nuget.org/packages/FlatFile.FixedLength.Attributes):

```sh
Install-Package FlatFile.FixedLength.Attributes
```

This commands from Package Manager Console will download and install FlatFile and all required dependencies.

### Benchmarks
#### Simple write
| Name                         | Milliseconds | Percent  |
|------------------------------|--------------|----------|
| FileHelperEngine.WriteStream | 5175         | 11266.8% |
| FlatFileEngine.Write         | 45           | 100%     |

#### Simple read
| Name                        | Milliseconds | Percent |
|-----------------------------|--------------|---------|
| FileHelperEngine.ReadStream | 7636         | 2764.4% |
| FlatFileEngine.Read         | 276          | 100%    |

#### Big (100000 entities) write
| Name                         | Milliseconds | Percent |
|------------------------------|--------------|---------|
| FileHelperEngine.WriteStream | 17246        | 838.4%  |
| FlatFileEngine.Write         | 2057         | 100%    |

#### Big (100000 entities) write with reflection magic
| Name                         | Milliseconds | Percent |
|------------------------------|--------------|---------|
| FileHelperEngine.WriteStream | 17778        | 1052.5% |
| FlatFileEngine.Write         | 1689         | 100%    |

#### FlatFile vs CsvHelper
##### Write all records with class mapping
| Name                   | Milliseconds | Percent |
|------------------------|--------------|---------|
| CsvWriter.WriteRecords | 26578        | 7988.8% |
| FlatFileEngine.Write   | 332          | 100%    |  

##### Read all records with class mapping
| Name                   | Milliseconds | Percent |
|------------------------|--------------|---------|
| CsvWriter.WriteRecords | 18795        | 3190.5% |
| FlatFileEngine.Write   | 589          | 100%    |  

### Usage
#### Class mapping 
##### DelimitedLayout
```cs
public sealed class DelimitedSampleRecordLayout : DelimitedLayout<FixedSampleRecord>
{
    public DelimitedSampleRecordLayout()
    {
        this.WithDelimiter(";")
            .WithQuote("\"")
            .WithMember(x => x.Cuit)
            .WithMember(x => x.Nombre)
            .WithMember(x => x.Actividad, c => c.WithName("AnotherName"));
    }
}
```
##### FixedLayout
```cs
public sealed class FixedSampleRecordLayout : FixedLayout<FixedSampleRecord>
{
    public FixedSampleRecordLayout()
    {
        this.WithMember(x => x.Cuit, c => c.WithLength(11))
            .WithMember(x => x.Nombre, c => c.WithLength(160))
            .WithMember(x => x.Actividad, c => c.WithLength(6));
    }
}
```

#### Run-time mapping
##### DelimitedLayout
```cs
public class LayoutFactory
{
    public IDelimitedLayout<TestObject> GetLayout()
    {
        IDelimitedLayout<TestObject> layout = new DelimitedLayout<TestObject>()
            .WithDelimiter(";")
            .WithQuote("\"")
            .WithMember(o => o.Id)
            .WithMember(o => o.Description)
            .WithMember(o => o.NullableInt, set => set.AllowNull("=Null"));

        return layout;
    } 
}
```
##### FixedLayout
```cs
public class LayoutFactory
{
    public IFixedLayout<TestObject> GetLayout()
    {
        IFixedLayout<TestObject> layout = new FixedLayout<TestObject>()
            .WithMember(o => o.Id, set => set
                .WithLength(5)
                .WithLeftPadding('0'))
            .WithMember(o => o.Description, set => set
                .WithLength(25)
                .WithRightPadding(' '))
            .WithMember(o => o.NullableInt, set => set
                .WithLength(5)
                .AllowNull("=Null")
                .WithLeftPadding('0'));

        return layout;
    }
```

#### Attribute mapping
##### Delimited
```cs
using FlatFile.Delimited.Attributes;

[DelimitedFile(Delimiter = ";", Quotes = "\"")]
public class TestObject
{
    [DelimitedField(1)]
    public int Id { get; set; }

    [DelimitedField(2)]
    public string Description { get; set; }

    [DelimitedField(3, NullValue = "=Null")]
    public int? NullableInt { get; set; }
}
```

##### Fixed
```cs
using FlatFile.FixedLength;
using FlatFile.FixedLength.Attributes;

[FixedLengthFile]
public class TestObject
{
    [FixedLengthField(1, 5, PaddingChar = '0')]
    public int Id { get; set; }

    [FixedLengthField(2, 25, PaddingChar = ' ', Padding = Padding.Right)]
    public string Description { get; set; }

    [FixedLengthField(2, 5, PaddingChar = '0', NullValue = "=Null")]
    public int? NullableInt { get; set; }
}
```

#### Read from stream
##### With layout
```cs
var layout = new FixedSampleRecordLayout();
var factory = new FixedLengthFileEngineFactory();
using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(FixedFileSample)))
{
    var flatFile = factory.GetEngine(layout);

    var records = flatFile.Read<FixedSampleRecord>(stream).ToArray();
}
```

##### With attribute-mapping
```cs
var factory = new FixedLengthFileEngineFactory();
using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(FixedFileSample)))
{
    var flatFile = factory.GetEngine<FixedSampleRecord>();

    var records = flatFile.Read<FixedSampleRecord>(stream).ToArray();
}
```

##### With multiple fixed record types
```cs
var factory = new FixedLengthFileEngineFactory();
using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(FixedFileSample)))
{
    // If using attribute mapping, pass an array of record types
    // rather than layout instances
    var layouts = new ILayoutDescriptor<IFixedFieldSettingsContainer>[]
    {
        new HeaderRecordLayout(),
        new DetailRecordLayout(),
        new TrailerRecordLayout()
    };
    var flatFile = factory.GetEngine(layouts,
        line =>
        {
            // For each line, return the proper record type.
            // The mapping for this line will be loaded based on that type.
            // In this simple example, the first character determines the
            // record type.
            if (String.IsNullOrEmpty(line) || line.Length < 1) return null;
            switch (line[0])
            {
                case 'H':
                    return typeof (HeaderRecord);
                case 'D':
                    return typeof (DetailRecord);
                case 'T':
                    return typeof (TrailerRecord);
            }
            return null;
        });

    flatFile.Read(stream);
    var header = flatFile.GetRecords<HeaderRecord>().FirstOrDefault();
    var records = flatFile.GetRecords<DetailRecord>();
    var trailer = flatFile.GetRecords<TrailerRecord>().FirstOrDefault();
}
```

#### Write to stream
##### With layout
```cs
var sampleRecords = GetRecords();
var layout = new FixedSampleRecordLayout();
var factory = new FixedLengthFileEngineFactory();
using (var stream = new MemoryStream())
{
    var flatFile = factory.GetEngine(layout);

    flatFile.Write<FixedSampleRecord>(stream, sampleRecords);
}
```

##### With attribute-mapping
```cs
var sampleRecords = GetRecords();
var factory = new FixedLengthFileEngineFactory();
using (var stream = new MemoryStream())
{
    var flatFile = factory.GetEngine<FixedSampleRecord>();

    flatFile.Write<FixedSampleRecord>(stream, sampleRecords);
}
```
