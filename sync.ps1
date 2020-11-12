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



$push = New-Object System.Management.Automation.Host.ChoiceDescription '&1.Push', 'Push latest items to Sitecore...'
$pull = New-Object System.Management.Automation.Host.ChoiceDescription '&2.Pull', 'Pull latest items from Sitecore...'
$options = [System.Management.Automation.Host.ChoiceDescription[]]($push, $pull)
$result = $host.ui.PromptForChoice('Sitecore Serialization?', 'How would you like to sync?', $options, 0)

switch ($result)
    {
        0 {
            Write-Host "Pushing latest items to Sitecore..." -ForegroundColor Green
            dotnet sitecore ser push
            if ($LASTEXITCODE -ne 0) {
                Write-Error "Serialization push failed, see errors above."
            }

            dotnet sitecore publish
            if ($LASTEXITCODE -ne 0) {
                Write-Error "Item publish failed, see errors above."
            }

        }
        1 {
            Write-Host "Pulling latest items from Sitecore..." -ForegroundColor Green
            dotnet sitecore ser pull
            if ($LASTEXITCODE -ne 0) {
                Write-Error "Serialization pull failed, see errors above."
            }
        }
    }
