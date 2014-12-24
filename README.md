FlatFile [![Build status](https://ci.appveyor.com/api/projects/status/9uoix14g3w0rac3q?svg=true)](https://ci.appveyor.com/project/forcewake/flatfile)
========

FlatFile is a library to work with flat files

### Installing FlatFile
#### Installing FlatFile.Delimited
You should install [FlatFile.Delimited with NuGet](https://www.nuget.org/packages/FlatFile.Delimited):

```sh
Install-Package FlatFile.Delimited
```

#### Installing FlatFile.FixedLength
You should install [FlatFile.FixedLength with NuGet](https://www.nuget.org/packages/FlatFile.FixedLength):

```sh
Install-Package FlatFile.FixedLength
```

This command from Package Manager Console will download and install FlatFile and all required dependencies.

### Benchmarks
#### Simple write

```
Name                                    Milliseconds        Percent                       
FileHelperEngine.WriteStream            5175                11266.8%                      
FlatFileEngine.Write                    45                  100%                          
```

#### Simple read
```
Name                                    Milliseconds        Percent                       
FileHelperEngine.ReadStream             7636                2764.4%                       
FlatFileEngine.Read                     276                 100%                          
```

#### Big (100000 entities) write
```
Name                                    Milliseconds        Percent                       
FileHelperEngine.WriteStream            17246               838.4%                        
FlatFileEngine.Write                    2057                100%                          
```

#### Big (100000 entities) write with reflection magic
```
Name                                    Milliseconds        Percent                       
FileHelperEngine.WriteStream            17778               1052.5%                       
FlatFileEngine.Write                    1689                100%                          
```

### Usage
#### Class mapping 
##### DelimitedLayout
```cs
public class DelimitedSampleRecordLayout : DelimitedLayout<FixedSampleRecord>
{
    protected override void MapLayout()
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
public class FixedSampleRecordLayout : FixedLayout<FixedSampleRecord>
{
    protected override void MapLayout()
    {
        this.WithMember(x => x.Cuit, c => c.WithLenght(11))
            .WithMember(x => x.Nombre, c => c.WithLenght(160))
            .WithMember(x => x.Actividad, c => c.WithLenght(6));
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
            .WithMember(o => o.Id, set => set.WithLenght(5).WithLeftPadding('0'))
            .WithMember(o => o.Description, set => set.WithLenght(25).WithRightPadding(' '))
            .WithMember(o => o.NullableInt, set => set.WithLenght(5).AllowNull("=Null").WithLeftPadding('0'));

        return layout;
    } 
}
```
#### Read from stream
```cs
var layout = new FixedSampleRecordLayout();
using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(FixedFileSample)))
{
    var flatFile = new FixedLengthFileEngine<FixedSampleRecord>();

    var records = flatFile.Read(layout, stream).ToArray();

    records.Should().HaveCount(19);
}
```
#### Write to stream
```cs
var layout = new FixedSampleRecordLayout();
using (var stream = new MemoryStream())
{
    var flatFile = new FixedLengthFileEngine<FixedSampleRecord>();

    flatFile.Write(layout, stream, sampleRecords);
}
```