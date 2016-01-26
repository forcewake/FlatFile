Properties {
    $solution = "FlatFile.sln"
}

$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'

Include "$root\assets\psake-common.ps1"

Task Default -Depends Pack

Task Pack -Depends Compile -Description "Create NuGet packages and archive files." {
    $version = Get-BuildVersion
    $releaseNotes = Get-ReleaseNotes    
    
    $projects = @(
        "FlatFile.Core", 
        "FlatFile.Core.Attributes", 
        "FlatFile.Delimited",
        "FlatFile.FixedLength",
        "FlatFile.Delimited.Attributes",
        "FlatFile.FixedLength.Attributes",
        "FlatFile"
        )
    
    $projects | ForEach {
        Create-Package $_ $version $releaseNotes
    }
}