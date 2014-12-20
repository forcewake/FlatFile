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
EventedFlatFileEngine.Write             63                  137.6%                        
```

#### Simple read
```
Name                                    Milliseconds        Percent                       
FileHelperEngine.ReadStream             7636                2764.4%                       
FlatFileEngine.Read                     276                 100%                          
EventedFlatFileEngine.Read              309                 112.1%               
```

#### Big (100000 entities) write
```
Name                                    Milliseconds        Percent                       
FileHelperEngine.WriteStream            17246               838.4%                        
FlatFileEngine.Write                    2057                100%                          
EventedFlatFileEngine.Write             2967                144.3%                        
```

#### Big (100000 entities) write with reflection magic
```
Name                                    Milliseconds        Percent                       
FileHelperEngine.WriteStream            17778               1052.5%                       
FlatFileEngine.Write                    1689                100%                          
EventedFlatFileEngine.Write             2334                138.2%     
```

### Usage

Create layout for the entity

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

Write array to the stream

```cs
var layout = new FixedSampleRecordLayout();
using (var stream = new MemoryStream())
using (var flatFile = new FlatFileEngine<FixedSampleRecord>(
    stream,
    new FixedLengthLineParser<FixedSampleRecord>(layout),
    new FixedLengthLineBuilder<FixedSampleRecord>(layout)))
{
    flatFile.Write(sampleRecords);
}
```