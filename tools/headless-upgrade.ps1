############################################################
#   A Powershell Library to update your Helixbase solution #
#   to a Helixbase Headless solution                       #
############################################################
[CmdletBinding()]
param()

# Pre-Requisites
# 1. All projects are using the new csproj structure
# 2. PackageReference format instead of legacy packages.confif (https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference)
# 3. DotNet Framework 4.8 and NetCore 3.1 is installed

# Enable test run to only run report and make no file changes
$testRun = $false

# Solution and Project Info
$solutionName = "Helixbase"
$solutionFileExtension = "sln"
$projectFileExtension = "csproj"
$buildPropsExtension = "props"
$solutionRootPath = Split-Path -Path $PSScriptRoot -Parent
$sourceFolder = Join-Path -Path $solutionRootPath -ChildPath "src"
$solutionPath = "$solutionRootPath\$solutionName.$solutionFileExtension"

# Target Frameworks
$renderingModuleTargetFramework = "netcoreapp3.1"
$platformModuleTargetFramework = "net48"

# Module Suffixes
$renderingModuleSuffix = "Rendering"
$platformModuleSuffix = "Platform"
$testProjectSuffix = "Tests"

# Legacy Folders
$websiteModuleFolder = "website"
$testsModuleFolder = "tests"

# New Folders
$renderingModuleFolder = "rendering"
$platformModuleFolder = "platform"
$platformTestsModuleFolder = "platform.tests"

# Nuget Packages
$reneringHostPackages = @(
    'Sitecore.AspNet.RenderingEngine',
    'Sitecore.LayoutService.Client'
)

$hppReneringHostPackages = @(
    'RichardSzalay.Helix.Publishing.WebRoot'
)

# Helix Layer Names
$layers = @(
    'Feature',
    'Foundation',
    'Project'
)

# Helix Publishing Pipeline
$hppLayer = "Website"
$websiteHppMobulePath = Join-Path -Path $sourceFolder -ChildPath $hppLayer

# Steps
# 1. Iterate Solution Layers (Feature/Foundation/Project)
# 2. Convert existing Website projects into DotNet Framework 4.8 Platform (Sitecore) project
# 3. Create new DotNetCore Rendering Host project for each module
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
    
    if($currentFrameworkNode.InnerText -eq $NewTargetFramework) {
        return
    }

    Write-Verbose "Updating framework from $($currentFrameworkNode.InnerText) to $NewTargetFramework"
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

    # Exit if path doesn't exist
    if(-Not (Test-Path $ProjectDirectory))
    {
        Write-Verbose "Project directory doesn't exist $ProjectDirectory"
        return
    }

    # Move Files
    Write-Verbose "Moving Project from $ProjectDirectory to $NewProjectDirectory"
    if(-Not($testRun)) {
        if(-Not (Test-Path $movedProjectPath)) {
            if(Test-Path $NewProjectDirectory) {
                Remove-Item -Path $NewProjectDirectory -Recurse -Force -ErrorAction SilentlyContinue
            }
            Rename-Item -Path $ProjectDirectory -NewName $NewProjectDirectory -Force
        } 
    }

    # Rename Project File
    Write-Verbose "Updating Project Name from $ProjectName to $NewProjectName"
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
    Write-Verbose "Updating solution references from $projectRelativePath to $newProjectRelativePath"
    if(-Not($testRun)) {
        (Get-Content "$solutionPath").replace($projectRelativePath, $newProjectRelativePath) | Set-Content $solutionPath
    }

    # Update Projects References
    Write-Verbose "Updating Project references"
    if(-Not($testRun)) {
        (Get-Content "$newProjectPath").replace(".csproj", ".$NewProjectSuffix.csproj").replace("\$ModuleFolderName\","\$NewModuleFolderName\") | Set-Content $newProjectPath
    }
}

# Creates new Helix Module Project and adds to the solution
# ProjectType defaults to Class Library but could be web (DotNetCore WebApp)
Function Invoke-CreateSolutionProject {
    param(
        [Parameter(Mandatory=$false)]
        [ValidateSet("classlib","web")]
        [string]$ProjectType = "classlib",
        [Parameter(Mandatory=$true)]
        [string]$ProjectDirectory,
        [Parameter(Mandatory=$true)]
        [string]$ProjectName,
        [Parameter(Mandatory=$true)]
        [string]$LayerName,
        [Parameter(Mandatory=$false)]
        [string]$ModuleName,
        [Parameter(Mandatory=$true)]
        [string]$TargetFramework,
        [Parameter(Mandatory=$true)]
        [string[]]$Packages
    )

    # Exit if path doesn't exist
    if(Test-Path (Join-Path -Path $ProjectDirectory -ChildPath "$ProjectName.$projectFileExtension"))
    {
        Write-Verbose "New project already exists. Will not create $ProjectName"
        return
    }

    if(-Not($testRun)) {
        
        Write-Verbose "Creating new project $ProjectName" 
        dotnet new $ProjectType `
            --name $ProjectName `
            --type project `
            --target-framework-override $TargetFramework `
            --output $ProjectDirectory `
            | Out-Null

        Write-Verbose "Installing packges into $ProjectName"
        $Packages | ForEach-Object {
            dotnet add $ProjectDirectory package $_  | Out-Null
        }
        
        Write-Verbose "Adding $ProjectName to the solution $solutionName" 
        if($null -eq $ModuleName) {
            $solutionFolder = $LayerName
        } else {
            $solutionFolder = "$LayerName\$ModuleName"
        }
       
        dotnet sln $solutionPath `
            add --solution-folder $solutionFolder $ProjectDirectory `
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
    $websiteTestsProjectName = "$solutionName.$LayerName.$ModuleName.$testProjectSuffix"
    $renderingProjectName = "$solutionName.$LayerName.$ModuleName.$renderingModuleSuffix"
    $platformProjectName = "$solutionName.$LayerName.$ModuleName.$platformModuleSuffix"
    $platformTestsProjectName = "$platformProjectName.$testProjectSuffix"

    #  Get the project file path and check it exists
    $websiteProjectPath = Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\$websiteModuleFolder\$websiteProjectName.$projectFileExtension"
    if(-Not (Test-Path $websiteProjectPath)) {
        Write-Verbose "Could not find module project file to update TargetFramework: $websiteProjectPath"
    
    } else {
        # Update Project Target Framework to use platformModuleTargetFramework (netcore)
        Invoke-UpdatetProjectTargetFramework `
            -ProjectPath $websiteProjectPath `
            -NewTargetFramework $platformModuleTargetFramework
    } 
    
    # Migrate Website Project to Platform Project
    Invoke-MigrateProject `
        -ProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\$websiteModuleFolder") `
        -NewProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\$platformModuleFolder") `
        -ModuleFolderName $websiteModuleFolder `
        -NewModuleFolderName $platformModuleFolder `
        -ProjectName $websiteProjectName `
        -NewProjectName $platformProjectName `
        -NewProjectSuffix $platformModuleSuffix

    # Migrate Test Project to Platform.Tests Project
    Invoke-MigrateProject `
        -ProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\$testsModuleFolder") `
        -NewProjectDirectory (Join-Path -Path $sourceFolder -ChildPath "$LayerName\$ModuleName\$platformTestsModuleFolder") `
        -ModuleFolderName $websiteModuleFolder `
        -NewModuleFolderName $platformModuleFolder `
        -ProjectName $websiteTestsProjectName `
        -NewProjectName $platformTestsProjectName `
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

Function Invoke-UpdateHppBuildProps {
    param(
        [Parameter(Mandatory=$true)]
        [string]$ProjectDirectory,
        [Parameter(Mandatory=$true)]
        [string]$RenderingDirectory
    )

    Get-ChildItem -Path $ProjectDirectory -Filter "*.$buildPropsExtension" -File | ForEach-Object {
        Write-Verbose "Updating Build Property $($_.FullName) value from $websiteModuleFolder to $platformModuleFolder"
        Write-Verbose "Updating Build Property $($_.FullName) value from $websiteModuleFolder to $renderingModuleFolder"
        (Get-Content $_.FullName).replace("\$websiteModuleFolder\", "\$platformModuleFolder\") | Set-Content $_.FullName
        (Get-Content $_.FullName).replace("\$websiteModuleFolder\", "\$renderingModuleFolder\") | Set-Content (Join-Path -Path $RenderingDirectory -ChildPath $_.Name)
    }
}

Function Invoke-SetupHppWebsites {
    param()

    Write-Host "`n-----------------------------------`n Updating Helix Publishing Pipelines `n-----------------------------------" -ForegroundColor Cyan

    $hppWebsiteProjectName = "$solutionName.$hppLayer"
    $hppPlatformProjectName = "$solutionName.$platformModuleSuffix"
    $hppRenderingProjectName = "$solutionName.$renderingModuleSuffix"


    $hppPlatformNewDirectory = Join-Path -Path $websiteHppMobulePath -ChildPath $platformModuleFolder
    $hppRenderingNewDirectory = Join-Path -Path $websiteHppMobulePath -ChildPath $renderingModuleFolder

    # Convert Website HPP Site to Platform HPP Site
    Invoke-MigrateProject `
        -ProjectDirectory (Join-Path -Path $websiteHppMobulePath -ChildPath $websiteModuleFolder) `
        -NewProjectDirectory $hppPlatformNewDirectory `
        -ModuleFolderName $websiteModuleFolder `
        -NewModuleFolderName $platformModuleFolder `
        -ProjectName $hppWebsiteProjectName `
        -NewProjectName $hppPlatformProjectName `
        -NewProjectSuffix $platformModuleSuffix

    # Create Rendering Host HPP Site
     Invoke-CreateSolutionProject `
        -ProjectDirectory $hppRenderingNewDirectory `
        -ProjectName $hppRenderingProjectName `
        -LayerName $hppLayer `
        -ProjectType "web" `
        -TargetFramework $renderingModuleTargetFramework `
        -Packages $hppReneringHostPackages

    # Update HPP Platform Build Properties
    Invoke-UpdateHppBuildProps `
        -ProjectDirectory $hppPlatformNewDirectory `
        -RenderingDirectory $hppRenderingNewDirectory
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

    # Update Helix Publishing Pipeline Projects
    Invoke-SetupHppWebsites

}

#Invoke-SetupHppWebsites
Invoke-Run