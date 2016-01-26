Properties {
    ### Directories
    $base_dir = $root
    $build_dir = "$base_dir\build"
    $src_dir = "$base_dir\src"
    $package_dir = "$src_dir\packages"
    $nuspec_dir = "$base_dir\nuspecs"
    $temp_dir = "$build_dir\Temp"
    $framework_dir =  $env:windir + "\Microsoft.Net\Framework\v4.0.30319"

    ### Tools
    $nuget = "$src_dir\.nuget\nuget.exe"
    $xunit = "$package_dir\xunit.runners*\tools\xunit.console.clr4.exe"
    $7zip = "$package_dir\7-Zip.CommandLine.*\tools\7za.exe"

    ### AppVeyor-related
    $appVeyorConfig = "$base_dir\appveyor.yml"
    $appVeyor = $env:APPVEYOR

    ### Project information
    $solution_path = "$src_dir\$solution"
	$sharedAssemblyInfo = "$src_dir\SharedAssemblyInfo.cs"
    $config = "Release"    
	$frameworks = @("NET35", "NET40", "NET45")
    
    ### Files
    $releaseNotes = "$base_dir\ChangeLog.md"
}

## Tasks

Task Restore -Description "Restore NuGet packages for solution." {
    "Restoring NuGet packages for '$solution_path'..."
    Exec { .$nuget restore $solution_path }
}

Task Clean -Description "Clean up build and project folders." {
    Clean-Directory $build_dir

    if ($solution) {
        "Cleaning up '$solution'..."
        
		foreach ($framework in $frameworks) {
			Exec { msbuild $solution_path /target:Clean /nologo /verbosity:minimal /p:Framework=$framework}
		}
    }
}

Task Compile -Depends Clean, Restore -Description "Compile all the projects in a solution." {
    "Compiling '$solution'..."

    $extra = $null
    if ($appVeyor) {
        $extra = "/logger:C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
    }

	foreach ($framework in $frameworks) {
		Exec { msbuild $solution_path /p:"Configuration=$config;Framework=$framework" /nologo /verbosity:minimal $extra }
	}
}

### Pack functions

function Create-Package($project, $version, $notes) {
    Create-Directory $temp_dir
    Copy-Files "$nuspec_dir\$project.nuspec" $temp_dir

    Try {
        Replace-Content "$nuspec_dir\$project.nuspec" '#releaseNotes#' $notes
        
        Replace-Content "$nuspec_dir\$project.nuspec" '$version$' $version
        
        Exec { .$nuget pack "$nuspec_dir\$project.nuspec" -OutputDirectory "$build_dir" -BasePath "$build_dir" -Version $version}
    }
    Finally {
        Move-Files "$temp_dir\$project.nuspec" $nuspec_dir
    }
}

function Get-ReleaseNotes {
    $content = (Get-Content "$releaseNotes")  -Join "`n"
    return $content
}

### Version functions

function Get-BuildVersion {
    $version = Get-SharedVersion
    $buildVersion = $env:APPVEYOR_BUILD_VERSION

    if ($buildVersion -ne $null) {
        $version = $buildVersion
    }

    return $version
}

function Get-SharedVersion {
    $line = Get-Content "$sharedAssemblyInfo" | where {$_.Contains("AssemblyVersion")}
    $line.Split('"')[1]
}

function Update-AppveyorVersion($version) {
    Check-Version($version)

    $versionPattern = "version: [0-9]+(\.([0-9]+|\*)){1,3}"
    $versionReplace = "version: $version"

    if (Test-Path $appVeyorConfig) {
        "Patching $appVeyorConfig..."
        Replace-Content "$appVeyorConfig" $versionPattern $versionReplace
    }
}


### Common functions

function Create-Directory($dir) {
    New-Item -Path $dir -Type Directory -Force > $null
}

function Clean-Directory($dir) {
    If (Test-Path $dir) {
        "Cleaning up '$dir'..."
        Remove-Item "$dir\*" -Recurse -Force
    }
}

function Copy-Files($source, $destination) {
    Copy-Item "$source" $destination -Force > $null
}

function Move-Files($source, $destination) {
    Move-Item "$source" $destination -Force > $null
}

function Replace-Content($file, $pattern, $substring) {
    (gc $file) -Replace $pattern, $substring | sc $file
}
