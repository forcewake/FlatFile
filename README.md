FluentFiles 
========
[![Build status](https://ci.appveyor.com/api/projects/status/s52n84t6o4nr92c3?svg=true)](https://ci.appveyor.com/project/mthamil/fluentfiles)

FluentFiles is a fork of the [FlatFile](https://github.com/forcewake/FlatFile) project. 
It is a library for reading and writing fixed-width and delimited text files.

### Installing FluentFiles

#### Installing all packages
The following installs the [FluentFiles](https://www.nuget.org/packages/FluentFiles) metapackage that pulls in all other packages:

```sh
dotnet add package FluentFiles
```


#### Installing FluentFiles.Delimited
You should install [FluentFiles.Delimited with NuGet](https://www.nuget.org/packages/FluentFiles.Delimited):

```sh
dotnet add package FluentFiles.Delimited
```

##### Add attribute-mapping extensions
You should install [FluentFiles.Delimited.Attributes with NuGet](https://www.nuget.org/packages/FluentFiles.Delimited.Attributes):

```sh
dotnet add package FluentFiles.Delimited.Attributes
```

#### Installing FluentFiles.FixedLength
You should install [FluentFiles.FixedLength with NuGet](https://www.nuget.org/packages/FluentFiles.FixedLength):

```sh
dotnet add packageFluentFiles.FixedLength
```

##### Add attribute-mapping extensions
You should install [FluentFiles.FixedLength.Attributes with NuGet](https://www.nuget.org/packages/FluentFiles.FixedLength.Attributes):

```sh
dotnet add package FluentFiles.FixedLength.Attributes
```

These commands will download and install FluentFiles and all required dependencies.


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
            .WithMember(x => x.Id)
            .WithMember(x => x.Name)
            .WithMember(x => x.Description, c => c.WithName("AnotherName"));
    }
}
```
##### FixedLayout
```cs
public sealed class FixedSampleRecordLayout : FixedLayout<FixedSampleRecord>
{
    public FixedSampleRecordLayout()
    {
        this.WithMember(x => x.Id, c => c.WithLength(11))
            .WithMember(x => x.Name, c => c.WithLength(160))
            .WithMember(x => x.Description, c => c.WithLength(6));
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
            .WithMember(x => x.Id)
            .WithMember(x => x.Description)
            .WithMember(x => x.NullableInt, c => c.AllowNull("=Null"));

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
            .WithMember(x => x.Id, c => c
                .WithLength(5)
                .WithLeftPadding('0'))
            .WithMember(x => x.Description, c => c
                .WithLength(25)
                .WithRightPadding(' '))
            .WithMember(x => x.NullableInt, c => c
                .WithLength(5)
                .AllowNull("=Null")
                .WithLeftPadding('0'));

        return layout;
    }

    // You can also register a StringNormalizer function to convert input into the FixedLengthLineBuilder
    // to a string compatible with the specifications for your target file. 
    //
    // Note that the StringNormalizer function is only used when writing files, not when reading files.
    //
    // Example:
    public IFixedLayout<TestObject> GetLayout()
    {
        IFixedLayout<TestObject> layout = new FixedLayout<TestObject>()
            .WithMember(x => x.Description, c => c
                .WithLength(25)
                .WithRightPadding(' ')
                .WithStringNormalizer(input => {
                     // the normalization to FormD splits accented letters in accents+letters,
                     // the rest aftet that removes those accents (and other non-spacing characters) from the ouput
                     // So unicode L'été becomes L'ete
                     return new string(
                        input.Normalize(System.Text.NormalizationForm.FormD)
                             .ToCharArray()
                             .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
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
