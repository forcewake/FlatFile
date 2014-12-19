@echo off

src\.nuget\NuGet.exe install src\.nuget\packages.config -OutputDirectory src\packages

powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& {Import-Module '.\src\packages\psake.4.4.1\tools\psake.psm1'; invoke-psake '.\assets\psake-project.ps1' %*; if ($LastExitCode -and $LastExitCode -ne 0) {write-host "ERROR CODE: $LastExitCode" -fore RED; exit $lastexitcode} }"
