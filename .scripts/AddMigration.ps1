param (
    [string]$MigrationName
)

if (-not $MigrationName) {
    Write-Host "Usage: $($MyInvocation.MyCommand.Name) <MigrationName>"
    exit 1
}

dotnet ef migrations add $MigrationName --project "Digital.Pages.Api" --context "DigitalPagesContext"
