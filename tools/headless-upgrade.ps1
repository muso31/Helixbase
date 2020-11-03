############################################################
#   A Powershell Library to update your Helixbase solution #
#   to a Helixbase Headless solution                       #
############################################################

# Pre-Requisites
# 1. All projects are using the new csproj structure
# 2. PackageReference format instead of legacy packages.confif (https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference)

# Enable test run to only run report and make no file changes
$testRun = $false

# Variables
$solutionName = "Helixbase"
$solutionFileExtension = "sln"
$projectFileExtension = "csproj"
$solutionRootPath = Split-Path -Path $PSScriptRoot -Parent
$sourceFolder = Join-Path -Path $solutionRootPath -ChildPath "src"
$solutionPath = "$solutionRootPath\$solutionName.$solutionFileExtension"

$renderingModuleTargetFramework = "netcoreapp3.1"
$platformModuleTargetFramework = "net48"


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

Function Invoke-UpdatetProjectTargetFramework {
    param(
        [Parameter(Mandatory=$true)]
        [string]$ProjectPath,
        [Parameter(Mandatory=$true)]
        [string]$NewTargetFramework    
    )

    #Update project TargetFramework to use netcore
    [xml]$xmlDoc = New-Object system.Xml.XmlDocument
    [xml]$xmlDoc = Get-Content $ProjectPath

    $currentFrameworkNode = $xmlDoc.SelectSingleNode("//Project/PropertyGroup/TargetFramework")
    Write-Host "Updating framework from $($currentFrameworkNode.InnerText) to $NewTargetFramework" -ForegroundColor Green

    # Set new TargetFramework
    $currentFrameworkNode.InnerText = $NewTargetFramework

    if(-Not $testRun) {
        $xmlDoc.Save($ProjectPath)
    }
}

# Moves Project into new directory
# Updates Project Name
# Updates Solution References
Function Invoke-MigrateProject {
    param(
        [Parameter(Mandatory=$true)]
        [string]$ProjectDirectory,
        [Parameter(Mandatory=$true)]
        [string]$NewProjectDirectory,
        [Parameter(Mandatory=$true)]
        [string]$ProjectName,
        [Parameter(Mandatory=$true)]
        [string]$NewProjectName
    )

    # Project Relative Paths
    $projectRelativePath = "$($ProjectDirectory.Replace("$solutionRootPath\", ''))\$ProjectName.$projectFileExtension";
    $newProjectRelativePath = "$($NewProjectDirectory.Replace("$solutionRootPath\", ''))\$NewProjectName.$projectFileExtension";

    # Move Files
    Write-Host "Moving Project from $ProjectDirectory to $NewProjectDirectory"
    if(-Not($testRun)) {
        if(-Not (Test-Path $NewProjectDirectory)) {
            New-Item -ItemType Directory -Path $NewProjectDirectory
        }
        if(-Not (Test-Path $ProjectDirectory)) {
            Write-Warning "Move did not happen: $ProjectDirectory doesn't exist."
        } else {
            Get-ChildItem -Path $ProjectDirectory -Recurse -File | Move-Item -Destination $NewProjectDirectory
            Get-ChildItem -Path $ProjectDirectory -Recurse -Directory | Remove-Item
        }
       
    }

    # Rename Project File
    Write-Host "Updating Project Name from $ProjectName to $NewProjectName"
    $newProjectPath = "$NewProjectDirectory\$ProjectName.$projectFileExtension"
    if(-Not($testRun)) {
        if(-Not (Test-Path $newProjectPath)) {
            Write-Warning "Can't rename project. $newProjectPath  doesn't exist"
        } else {
            Rename-Item `
                -Path $newProjectPath `
                -NewName "$NewProjectDirectory\$NewProjectName.$projectFileExtension"
        }
    }

    # Update Solution References
    Write-Host "Updating solution references from $projectRelativePath to $newProjectRelativePath"
    if(-Not($testRun)) {
        (Get-Content "$solutionPath").replace($projectRelativePath, $newProjectRelativePath) | Set-Content $solutionPath

    }
}

# Creates new Helix Module Project and adds to the solution
# TargetFramework defaults to net48 but can be overridden 
Function Invoke-CreateSolutionProject {
    param(
        [Parameter(Mandatory=$true)]
        [string]$ProjectDirectory,
        [Parameter(Mandatory=$true)]
        [string]$ProjectName,
        [Parameter(Mandatory=$true)]
        [string]$LayerName,
        [Parameter(Mandatory=$true)]
        [string]$ModuleName,
        [Parameter(Mandatory=$true)]
        [string]$TargetFramework
    )

    dotnet new classlib `
        --name $ProjectName `
        --type project `
        --target-framework-override $TargetFramework `
        --output $ProjectName

    dotnet sln $solutionPath `
        add --solution-folder "$LayerName\$ModuleName" $ProjectDirectory
}

# Converts a Helix Website (.Net Framework) Module into a NetCore Headless Rendering Module
Function Invoke-CovertToRenderingModule {
    param(
        [Parameter(Mandatory=$true)]
        [string]$LayerName,
        [Parameter(Mandatory=$true)]
        [string]$ModuleName    
    )

    $websiteProjectName = "$solutionName.$LayerName.$ModuleName"
    $renderingProjectName = "$solutionName.$LayerName.$ModuleName.Rendering"
    $platformProjectName = "$solutionName.$LayerName.$ModuleName.Platform"

    # Get the project file path and check it exists
    $websiteProjectPath = Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\website\$websiteProjectName.$projectFileExtension"
    if(-Not (Test-Path $websiteProjectPath)) {
        Write-Warning "Could not find module project file to update TargetFramework: $websiteProjectPath"
    
    } else {
        # Update Project Target Framework to use renderingModuleTargetFramework (netcore)
        Invoke-UpdatetProjectTargetFramework `
            -ProjectPath $websiteProjectPath `
            -NewTargetFramework $renderingModuleTargetFramework
    } 
   
    Invoke-MigrateProject `
        -ProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\website") `
        -NewProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\rendering") `
        -ProjectName $websiteProjectName `
        -NewProjectName $renderingProjectName

    # Create new Platform Project for Modue
    Write-Host "Create new new Platform Project $platformProjectName" 
    Invoke-CreateSolutionProject `
        -ProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\platform") `
        -ProjectName $platformProjectName `
        -LayerName $LayerName `
        -ModuleName $ModuleName `
        -TargetFramework $platformModuleTargetFramework
}


Function Invoke-Run {

    if($testRun) {
        Write-Host "Test run only" -ForegroundColor Red
    }

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