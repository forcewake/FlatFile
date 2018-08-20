FluentFiles 
========
[![Build status](https://ci.appveyor.com/api/projects/status/s52n84t6o4nr92c3?svg=true)](https://ci.appveyor.com/project/mthamil/fluentfiles)

FluentFiles is a fork of the [FlatFile](https://github.com/forcewake/FlatFile) project. It is library for working with flat files (up to 100 times faster then [FileHelpers](https://www.nuget.org/packages/FileHelpers/2.0.0))

### Installing FluentFiles

#### Installing all packages
You should install [FluentFiles with NuGet](https://www.nuget.org/packages/FluentFiles):

```sh
Install-Package FluentFiles
```

#### Installing FluentFiles.Delimited
You should install [FluentFiles.Delimited with NuGet](https://www.nuget.org/packages/FluentFiles.Delimited):

```sh
Install-Package FluentFiles.Delimited
```

##### Add attribute-mapping extensions
You should install [FluentFiles.Delimited.Attributes with NuGet](https://www.nuget.org/packages/FluentFiles.Delimited.Attributes):

```sh
Install-Package FluentFiles.Delimited.Attributes
```

#### Installing FluentFiles.FixedLength
You should install [FluentFiles.FixedLength with NuGet](https://www.nuget.org/packages/FluentFiles.FixedLength):

```sh
Install-Package FluentFiles.FixedLength
```

##### Add attribute-mapping extensions
You should install [FluentFiles.FixedLength.Attributes with NuGet](https://www.nuget.org/packages/FluentFiles.FixedLength.Attributes):

```sh
Install-Package FluentFiles.FixedLength.Attributes
```

These commands from Package Manager Console will download and install FluentFiles and all required dependencies.


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
| CsvWriter.ReadRecords | 18795        | 3190.5% |
| FlatFileEngine.Read   | 589          | 100%    |  

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

    // you can also register a StringNormalizer function to convert input into the FixedLengthLineBuilder
    // to a string compatible with the specifications for your target File. 
    //
    // Note that the StringNormalizer function is only used when creating/building files. Not when reading/parsing files.
    //
    // example:
    public IFixedLayout<TestObject> GetLayout()
    {
        IFixedLayout<TestObject> layout = new FixedLayout<TestObject>()
            .WithMember(o => o.Description, set => set
                .WithLength(25)
                .WithRightPadding(' ')
                .WithStringNormalizer((input) => {
                     // the normalization to FormD splits accented letters in accents+letters,
                     // the rest aftet that removes those accents (and other non-spacing characters) from the ouput
                     // So unicode L'été becomes L'ete
                     return new string(
                        input.Normalize(System.Text.NormalizationForm.FormD)
                             .ToCharArray()
                             .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                             .ToArray());
                }))

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
