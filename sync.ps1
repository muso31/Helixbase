[CmdletBinding()]
Param (
    [string]
    $HostName = "helixbase",
    [switch]
    $LoginToSitecore
)

dotnet tool restore

if($LoginToSitecore) {
    dotnet sitecore login --cm https://cm.$HostName.localhost/ --auth https://id.$HostName.localhost/ --allow-write true
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Unable to log into Sitecore, did the Sitecore environment start correctly? See logs above."
    }
}

Write-Host "Pushing latest items to Sitecore..." -ForegroundColor Green

dotnet sitecore ser push
if ($LASTEXITCODE -ne 0) {
    Write-Error "Serialization push failed, see errors above."
}

dotnet sitecore publish
if ($LASTEXITCODE -ne 0) {
    Write-Error "Item publish failed, see errors above."
}