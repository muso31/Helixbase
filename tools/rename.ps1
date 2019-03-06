# Modify Helixbase Solution and Helix Modules to match project specific requirements
Param(
        [Parameter(Position=0, Mandatory=$true)]
        [string]$ProjectName      
    )

# Variables to change
$originalName = "Helixbase"

function Rename-Files
{
    Param(
        [Parameter(Position=0, Mandatory=$true)]
        [string]$StartPath,
        [Parameter(Position=1, Mandatory=$true)]
        [string]$OldValue,
        [Parameter(Position=2, Mandatory=$true)]
        [string]$NewValue
    )

    $pattern = "*$OldValue*"

    $folderItems = Get-ChildItem -Directory -Path "$StartPath" -Recurse -Filter $pattern -Force | Where-Object { $_.FullName -notmatch "\\(obj|bin)\\?" } | Sort-Object { $_.FullName.Length } -Descending
    $folderItems | Rename-Item -NewName { $_.Name -replace $OldValue, $NewValue } -Force

    $fileItems = Get-ChildItem -File -Path "$StartPath" -Filter $pattern -Recurse -Force | Where-Object { $_.FullName -notmatch "\\(obj|bin)\\?" } 
    $fileItems | Rename-Item -NewName { $_.Name -replace $OldValue, $NewValue } -Force
}

function Update-FileContent
{
    Param(
        [Parameter(Position=0, Mandatory=$true)]
        [string]$StartPath,
        [Parameter(Position=1, Mandatory=$true)]
        [string]$OldValue,
        [Parameter(Position=2, Mandatory=$true)]
        [string]$NewValue,
        [Parameter(Position=3, Mandatory=$true)]
        [string]$FileExtensionsRegex
    )

    $filesToUpdate = Get-ChildItem -File -Path "$StartPath" -Recurse -Force | Where-Object { ( $_.FullName -notmatch "\\(obj|bin)\\?") -and ($_.Name -match $FileExtensionsRegex) } | Select-String -Pattern $OldValue | Group-Object Path | Select-Object -ExpandProperty Name
    $filesToUpdate | ForEach-Object { (Get-Content $_ ) -ireplace [regex]::Escape($OldValue), $NewValue | Set-Content $_ -Force }
}

try {
    $startPath = $PSScriptRoot

    # Set Variables 
    $fileExtensionsToUpdateContentRegex = "(.sln|.config|.csproj|.cs|.cshtml|.feature|.js|.nuspec|.role|.sitecore|.yml|.targets|.pubxml)$"
    $oldNamespacePrefix = $originalName
    $newNamespacePrefix = $ProjectName


    Rename-Files -StartPath "$startPath" -OldValue $oldNamespacePrefix -NewValue $newNamespacePrefix
    Update-FileContent -StartPath "$startPath" -OldValue $oldNamespacePrefix -NewValue $newNamespacePrefix -FileExtensionsRegex $fileExtensionsToUpdateContentRegex
    
    Write-Host "Solution and Project references renamed from $originalName to $ProjectName" -ForegroundColor Green

} catch {
    Write-Host "Modifying the Solution and Projects failed" -ForegroundColor Red
    Write-Host $($_.Exception.Message) -ForegroundColor Red
    Break
}
