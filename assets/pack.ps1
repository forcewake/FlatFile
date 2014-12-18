function Update-Version($path, $compiledPath, $version) {
	$content = (Get-Content $path) 
	$content = $content -replace '\$version\$', $version
	$content | Out-File $compiledPath
}

function Get-Version() {
	$version = [System.Reflection.Assembly]::LoadFile("$root\src\FlatFile.Core\bin\Release\NET35\FlatFile.Core.dll").GetName().Version
	$currentVersion = "{0}.{1}.{2}" -f ($version.Major, $version.Minor, $version.Build)
	return $currentVersion;
}

function Create-Package($path) {
	& $root\src\.nuget\NuGet.exe pack $path
}

$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'

$coreNuspecPath = "$root\assets\FlatFile.Core.nuspec"
$coreCompiledNuspecPath = "$root\assets\FlatFile.Core.Compiled.nuspec"

$delimitedNuspecPath = "$root\assets\FlatFile.Delimited.nuspec"
$delimitedCompiledNuspecPath = "$root\assets\FlatFile.Delimited.Compiled.nuspec"

$fixedLengthNuspecPath = "$root\assets\FlatFile.FixedLength.nuspec"
$fixedLengthCompiledNuspecPath = "$root\assets\FlatFile.FixedLength.Compiled.nuspec"

$version = Get-Version

Write-Host "Setting .nuspec version tag to $version"

Update-Version $coreNuspecPath $coreCompiledNuspecPath $version
Update-Version $delimitedNuspecPath $delimitedCompiledNuspecPath $version
Update-Version $fixedLengthNuspecPath $fixedLengthCompiledNuspecPath $version

Create-Package $coreCompiledNuspecPath
Create-Package $delimitedCompiledNuspecPath
Create-Package $fixedLengthCompiledNuspecPath