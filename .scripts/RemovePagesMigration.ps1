$DigitalScriptsPath = "${PSScriptRoot}/../Digital.Lib/.scripts"
$ConnectionString = & "${DigitalScriptsPath}/GetConnectionString.ps1" -ProjectPath "../Digital.Pages.Api" -env "Development"
$Project = "$PSScriptRoot/../Digital.Pages.Api"

dotnet ef migrations remove --project $Project -- $ConnectionString
