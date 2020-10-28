$ErrorActionPreference = "Stop";

. ".\docker\settings.ps1"

Invoke-ContainerLogin

# Double check whether init has been run
$envCheckVariable = "HOST_LICENSE_FOLDER"
$envCheck = Get-Content .env -Encoding UTF8 | Where-Object { $_ -imatch "^$envCheckVariable=.+" }
if (-not $envCheck) {
    throw "$envCheckVariable does not have a value. Did you run 'init.ps1 -InitEnv'?"
}

# Build all containers in the Sitecore instance, forcing a pull of latest base containers
Write-Host "Building containers..." -ForegroundColor Green
docker-compose build
if ($LASTEXITCODE -ne 0) {
    Write-Error "Container build failed, see errors above."
}

# Start the Sitecore instance
Write-Host "Starting Sitecore environment..." -ForegroundColor Green
docker-compose up -d

# Wait for Traefik to expose CM route
Write-Host "Waiting for CM to become available..." -ForegroundColor Green
$startTime = Get-Date
do {
    Start-Sleep -Milliseconds 100
    try {
        $status = Invoke-RestMethod "http://localhost:8079/api/http/routers/cm-secure@docker"
    } catch {
        if ($_.Exception.Response.StatusCode.value__ -ne "404") {
            throw
        }
    }
} while ($status.status -ne "enabled" -and $startTime.AddSeconds(15) -gt (Get-Date))
if (-not $status.status -eq "enabled") {
    $status
    Write-Error "Timeout waiting for Sitecore CM to become available via Traefik proxy. Check CM container logs."
}

##TODO THIS ISN'T WORKING.... 

# dotnet sitecore login --cm https://cm.$rootHostDomain/ --auth https://id.$rootHostDomain/ --allow-write true
# if ($LASTEXITCODE -ne 0) {
#     Write-Error "Unable to log into Sitecore, did the Sitecore environment start correctly? See logs above."
# }

# Write-Host "Pushing latest items to Sitecore..." -ForegroundColor Green

# dotnet sitecore ser push
# if ($LASTEXITCODE -ne 0) {
#     Write-Error "Serialization push failed, see errors above."
# }

# dotnet sitecore publish
# if ($LASTEXITCODE -ne 0) {
#     Write-Error "Item publish failed, see errors above."
# }

Write-Host "Opening site..." -ForegroundColor Green

Start-Process https://cm.$rootHostDomain/sitecore/
Start-Process https://www.$rootHostDomain/

Write-Host ""
Write-Host "Use the following command to monitor your Rendering Host:" -ForegroundColor Green
Write-Host "docker-compose logs -f rendering"
Write-Host ""