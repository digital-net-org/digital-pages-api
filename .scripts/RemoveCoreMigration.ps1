$DigitalScriptsPath = "${PSScriptRoot}/../Digital.Lib/.scripts"
$ConnectionString = & "${DigitalScriptsPath}/GetConnectionString.ps1" -ProjectPath "../Digital.Pages.Api" -env "Development"
& "${DigitalScriptsPath}/RemoveMigration.ps1" $ConnectionString
