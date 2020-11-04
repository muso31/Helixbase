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

# Folder Names
$websiteModuleFolder = "website"
$renderingModuleFolder = "rendering"
$platformModuleFolder = "platform"

# Nuget Packages
$reneringHostPackages = @(
    'Sitecore.AspNet.RenderingEngine',
    'Sitecore.LayoutService.Client'
)

$layers = @(
    'Feature',
    'Foundation',
    'Project'
)

# Steps
# 1. Iterate Solution Layers (Feature/Foundation/Project)
# 2. Convert existing Website projects into netcore Renderinng project
# 3. Create new .Net Framework 4.8 Platform project for each module

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
        [string]$ModuleFolderName,
        [Parameter(Mandatory=$true)]
        [string]$NewModuleFolderName,
        [Parameter(Mandatory=$true)]
        [string]$ProjectName,
        [Parameter(Mandatory=$true)]
        [string]$NewProjectName,
        [Parameter(Mandatory=$true)]
        [string]$NewProjectSuffix
    )

    # Project Relative Paths
    $projectFileName = "$ProjectName.$projectFileExtension"
    $newProjectFileName = "$NewProjectName.$projectFileExtension"
    $projectRelativePath = "$($ProjectDirectory.Replace("$solutionRootPath\", ''))\$projectFileName"
    $newProjectRelativePath = "$($NewProjectDirectory.Replace("$solutionRootPath\", ''))\$newProjectFileName"
    $movedProjectPath = "$NewProjectDirectory\$ProjectName.$projectFileExtension"
    $newProjectPath = "$NewProjectDirectory\$NewProjectName.$projectFileExtension"

    # Move Files
    Write-Host "Moving Project from $ProjectDirectory to $NewProjectDirectory"
    if(-Not($testRun)) {
        if(-Not (Test-Path $movedProjectPath)) {
            if(Test-Path $NewProjectDirectory) {
                Remove-Item -Path $NewProjectDirectory -Recurse -Force
            }
            Rename-Item -Path $ProjectDirectory -NewName $NewProjectDirectory -Force
        } 
    }

    # Rename Project File
    Write-Host "Updating Project Name from $ProjectName to $NewProjectName"
    if(-Not($testRun)) {
        if(-Not (Test-Path $movedProjectPath)) {
            Write-Warning "Can't rename project. $movedProjectPath  doesn't exist"
        } else {
            Rename-Item `
                -Path $movedProjectPath `
                -NewName "$NewProjectDirectory\$NewProjectName.$projectFileExtension"
        }
    }

    # Update Solution References
    Write-Host "Updating solution references from $projectRelativePath to $newProjectRelativePath"
    if(-Not($testRun)) {
        (Get-Content "$solutionPath").replace($projectRelativePath, $newProjectRelativePath) | Set-Content $solutionPath
    }

    # Update Projects References
    Write-Host "Updating Project references"
    if(-Not($testRun)) {
        (Get-Content "$newProjectPath").replace(".csproj", ".$NewProjectSuffix.csproj").replace("\$ModuleFolderName\","\$NewModuleFolderName\") | Set-Content $newProjectPath
    }
}

# Creates new Helix Module Project and adds to the solution
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
        [string]$TargetFramework,
        [Parameter(Mandatory=$true)]
        [string[]]$Packages
    )

    if(-Not($testRun)) {
        Write-Host "Creating new project $ProjectName" 
        dotnet new classlib `
            --name $ProjectName `
            --type project `
            --target-framework-override $TargetFramework `
            --output $ProjectDirectory `
            | Out-Null

        Write-Host "Installing packges into $ProjectName"
        $Packages | ForEach-Object {
            dotnet add $ProjectDirectory package $_  | Out-Null
        }
        
        Write-Host "Adding $ProjectName to the solution $solutionName" 
        dotnet sln $solutionPath `
            add --solution-folder "$LayerName\$ModuleName" $ProjectDirectory `
            | Out-Null
    }
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
    $renderingProjectName = "$solutionName.$LayerName.$ModuleName.$renderingModuleSuffix"
    $platformProjectName = "$solutionName.$LayerName.$ModuleName.$platformModuleSuffix"
   
    Invoke-MigrateProject `
        -ProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\$websiteModuleFolder") `
        -NewProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\$platformModuleFolder") `
        -ModuleFolderName $websiteModuleFolder `
        -NewModuleFolderName $platformModuleFolder `
        -ProjectName $websiteProjectName `
        -NewProjectName $platformProjectName `
        -NewProjectSuffix $platformModuleSuffix

    # Create new Rendering Project for Module
    Invoke-CreateSolutionProject `
        -ProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\$renderingModuleFolder") `
        -ProjectName $renderingProjectName `
        -LayerName $LayerName `
        -ModuleName $ModuleName `
        -TargetFramework $renderingModuleTargetFramework `
        -Packages $reneringHostPackages
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