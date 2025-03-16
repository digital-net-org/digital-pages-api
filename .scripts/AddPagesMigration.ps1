$DigitalScriptsPath = "${PSScriptRoot}/../Digital.Lib/.scripts"
$ConnectionString = & "${DigitalScriptsPath}/GetConnectionString.ps1" -ProjectPath "../Digital.Pages.Api" -env "Development"
$Project = "$PSScriptRoot/../Digital.Pages.Api"

$MigrationName = Read-Host "Enter migration name"
dotnet ef migrations add $MigrationName --project $Project --context "DigitalPagesContext" -- $ConnectionString
