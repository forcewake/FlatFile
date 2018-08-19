Properties {
    $solution = "FluentFiles.sln"
}

$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'

Include "$root\assets\psake-common.ps1"

Task Default -Depends Pack

Task Pack -Depends Compile -Description "Create NuGet packages and archive files." {
    $version = Get-BuildVersion
    $releaseNotes = Get-ReleaseNotes    
    
    $projects = @(
        "FluentFiles.Core", 
        "FluentFiles.Core.Attributes", 
        "FluentFiles.Delimited",
        "FluentFiles.FixedLength",
        "FluentFiles.Delimited.Attributes",
        "FluentFiles.FixedLength.Attributes",
        "FluentFiles"
        )
    
    $projects | ForEach {
        Create-Package $_ $version $releaseNotes
    }
}