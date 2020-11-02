############################################################
#   A Powershell Library to update your Helixbase solution #
#   to a Helixbase Headless solution                       #
############################################################

# Pre-Requisites
# 1. All projects are using the new csproj structure
# 2. PackageReference format instead of legacy packages.confif (https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference)

# Enable test run to only run report and make no file changes
$testRun = $true

# Variables
$solutionName = "Helixbase"
$solutionRootPath = Split-Path -Path $PSScriptRoot -Parent
$sourceFolder = Join-Path -Path $solutionRootPath -ChildPath "src"

$renderingModuleTargetFramework = "netcoreapp3.1"

$renderingModuleSuffix = "Rendering"
$platformModuleSuffix = "Platform"

$layers = @(
    'Feature',
    'Foundation',
    'Project'
)

# Steps
# 1. Iterate Solution Layers (Feature/Foundation/Project)
# 2. Convert existing Website projects into netcore Renderinng project
# 3. Create new .Net Framework 4.8 Platform project for each module

# Converts a Helix Website (.Net Framework) Module into a NetCore Headless Rendering Module
Function Invoke-CovertToRenderingModule {
    param(
        [Parameter(Mandatory=$true)]
        [string]$LayerName,
        [Parameter(Mandatory=$true)]
        [string]$ModuleName    
    )

    # Get the project file path and check it exists
    $websiteProjectPath = Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\website\$solutionName.$LayerName.$ModuleName.csproj"
    if(-Not (Test-Path $websiteProjectPath)) {
        Write-Error "Could not find module project to convert: $websiteProjectPath"
        return
    } 
    
    #Update project to be 
    $currentFrameworkNode = Select-Xml `
        -Path $websiteProjectPath `
        -XPath "/Project/PropertyGroup/TargetFramework" | Select-Object `
        -ExpandProperty Node
    Write-Host "Updating framework from $($currentFrameworkNode.InnerXml) to $renderingModuleTargetFramework" -ForegroundColor Green

    # $websiteProject = [xml](Get-Content $websiteProjectPath)
    # $currentFrameworkNode = $websiteProject.Project.PropertyGroup

    # Write-Host "Updating framework from $($currentFrameworkNode.value) to $renderingModuleTargetFramework"
    # Write-Host $currentFrameworkNode.

    # $node = $websiteProject.Project.PropertyGroup.TargetFramework | where {$_.V -eq 'ManagementServer'}

}


Function Invoke-Run {

    # Iterate Layers and Modules
    $layers | ForEach-Object {
        $layerName = $_
        Write-Host "`n-----------------------------------`n Updating $layerName Layer`n-----------------------------------" -ForegroundColor Cyan

        Get-ChildItem -Path (Join-Path -Path $sourceFolder -ChildPath $layerName) -Directory -Force -ErrorAction SilentlyContinue | ForEach-Object {
            $moduleName = $_
            Write-Host "`nUpdating $moduleName Module" -ForegroundColor Yellow
            Invoke-CovertToRenderingModule -LayerName $layerName -ModuleName $moduleName
        }

    }


}


Invoke-Run