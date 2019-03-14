param (
    [string] $msbuild = "msbuild.exe", # Location of msbuild.exe
    [string] $vstest = "vstest.console.exe", # Location of vstest.console.exe
    [switch] $DoBuildSolution, # If the solution should be built
    [switch] $DoUnitTests, # If the Unit Tests should be run
    [switch] $DoInstallSitecore, # If Sitecore should be installed using ParTech.SimpleInstallScripts
    [switch] $DoDeploySolution, # If the solution should be deployed to the Sitecore instance
    [switch] $DoIntegrationTests, # If the integration tests should be run (requires running Sitecore instance)
    [switch] $DoFunctionalTests, # If the functional (i.e. browser) tests should be run (requires running Sitecore instance)
    [switch] $DoCreateArtifacts, # If artifacts (WDP and Unicorn yml files) should be packaged
    [switch] $DoSyncUnicorn, # If a Unicorn sync should be run
    [switch] $IsAppVeyor, # If Appveyor is running this build
    [switch] $IsAzureDevops, # If Azure Devops is running this build
    [string] $Configuration = "Release", # The msbuild configuration (Debug or Release)
    [string] $UnicornSecret, # The Unicorn Shared Secret for syncing Sitecore Content. Will be generated if left empty
    [string] $SimpleInstallScriptsVersion = "0.0.80", # The compatible version of ParTech.SimpleInstallScripts

    # These are a direct mapping of Install-Sitecore9 from ParTech.SimpleInstallScripts
    [Parameter(Mandatory)] [string] $Prefix, # The Prefix that will be used on SOLR, Website and Database instances.
    [Parameter(Mandatory)] [string] $SitecoreVersion, # i.e. 901XM0, 910XP0, 911XP1, etc.
    [Parameter(Mandatory)] [string] $DownloadBase, # The location where WDPs, ZIPs, license.xml are stored
    [Parameter(Mandatory)] [string] $SQLServer, # The DNS name or IP of the SQL Instance.
    [Parameter(Mandatory)] [string] $SqlAdminUser, # A SQL user with sysadmin privileges.
    [Parameter(Mandatory)] [string] $SqlAdminPassword, # The password for $SQLAdminUser.
    [string] $SitecoreAdminPassword, # The Password for the Sitecore Admin User. This will be regenerated if left on the default.
    [string] $DriveLetter = "C", # The desired drive to install Sitecore on, and download assets
    [string] $SolrHost = "solr", # The hostname of the Solr server
    [string] $SolrPort = "8983", # The port of the Solr server
    [Hashtable] $Parameters = @{}, # Parameters for Install-SitecoreConfiguration
    [string[]] $Packages, # Packages to install
    [switch] $DoUninstall, # Uninstalls Sitecore instead of installing
    [switch] $DoInstallPrerequisites, # Install SIF, Solr, etc.
    [switch] $DoSitecorePublish, # If the Sitecore master database should be published to web
    [switch] $DoRebuildLinkDatabases, # If the Link Databases should be rebuilt
    [switch] $DoRebuildSearchIndexes, # If the Search Indexes should be rebuilt
    [switch] $DoDeployMarketingDefinitions # If the Marketing Definitions should be deployed
)

Set-StrictMode -Version 2.0

$ErrorActionPreference = "Stop"
$IsCi = $IsAppVeyor -Or $IsAzureDevops
$Logger = ""

if ($IsAppVeyor) {
    $Logger = "/logger:Appveyor"
    # TODO: Same for AzureDevops
}

Import-Module $PSScriptRoot\functions.psm1 -Force
$rootDir = Resolve-Path "$PSScriptRoot\..\"
$srcDir = Resolve-Path "$rootDir\src"
$solutionFile = Resolve-Path "$rootDir\Helixbase.sln"

if ($DoBuildSolution) {
    nuget restore $solutionFile 
    & $msbuild $solutionFile /v:m /m /p:Configuration=$Configuration
    if (!($LastExitCode -eq "0")) {
        throw "Build failed with exit code $LastExitCode"
    }
}

if ($DoUnitTests) {
    $TestDlls = Get-AllTestDllsAsQuotedStrings $srcDir $Configuration
    if ([string]::IsNullOrWhiteSpace($TestDlls)) {
        Write-Host "No test assemblies found"
        return
    }
    
    Invoke-Expression "`& `"$vstest`" $Logger /Parallel $testDlls"
    
    if (!($LastExitCode -eq "0")) {
        throw "Tests failed with exit code $LastExitCode"
    }
}

Get-PackageProvider -Name Nuget -ForceBootstrap
Install-Module ParTech.SimpleInstallScripts -RequiredVersion $SimpleInstallScriptsVersion -Force -SkipPublisherCheck -AllowClobber

if (!$UnicornSecret) {
    $UnicornSecret = -join ((48..57) + (97..122) | Get-Random -Count 64 | ForEach-Object {[char]$_})
}

$GivenParameters = $Parameters.Clone()

# Adds all parameters to the $Parameters variable to be splatted to various functions
foreach ($Key in $MyInvocation.MyCommand.Parameters.Keys) {
    $Value = Get-Variable $Key -ValueOnly -EA SilentlyContinue
    If ($Value) { $Parameters.$Key = $Value }
}

$Parameters.Parameters = $GivenParameters

# Enrich the $Parameters with default values from this version of Sitecore so we can use them later below
$Defaults = Get-DefaultSitecoreParameters $SitecoreVersion
foreach ($key in $Defaults.Keys) {
    If (!$Parameters.ContainsKey($key) -Or !$Parameters.$key) {
        $Parameters.$key = $ExecutionContext.InvokeCommand.ExpandString($Defaults.$key)
    }
}

if ($DoInstallSitecore) {
    $SitecoreParameters = Get-EffectiveParametersForCommand "Install-Sitecore91" $Parameters
    Install-Sitecore91 @SitecoreParameters

    Set-SourceFolder "$PSScriptRoot\SourceFolder.config" $Parameters.SitecorePath $srcDir
    Set-Unicorn-Shared-Secret "$PSScriptRoot\Unicorn.SharedSecret.config" $Parameters.SitecorePath $UnicornSecret
    Set-PublishUrl "$srcDir\Website\code\Properties\PublishProfiles\Local.pubxml" $Parameters.SitecorePath
}

if ($DoDeploySolution) {
    & $msbuild $solutionFile /t:Website\Helixbase_Website:WebPublish /v:m /m /p:Configuration=$Configuration`;PublishProfile=Local
    if (!($LastExitCode -eq "0")) {
        throw "Build failed with exit code $LastExitCode"
    }

    if (!$DoSyncUnicorn) {
        Test-Site $Parameters.SitecoreUrl
    }
}

if ($DoSyncUnicorn) {
    Sync-Unicorn -ControlPanelUrl "$($Parameters.SitecoreUrl)/unicorn.aspx" -SharedSecret $UnicornSecret
    Test-Site $Parameters.SitecoreUrl
}

if ($DoIntegrationTests) {
    # TODO:
}

if ($DoFunctionalTests) {
    # TODO:
}

if ($DoCreateArtifacts) {
    $outputDir = Join-Path $rootDir "output"
    Remove-Item $outputDir -Recurse -Force -ErrorAction SilentlyContinue
    & $msbuild $solutionFile /t:Website\Helixbase_Website:WebPublish /v:m /m /p:Configuration=$Configuration`;PublishProfile=Package`;DefaultPackageOutputDir=$outputDir
    if (!($LastExitCode -eq "0")) {
        throw "Build failed with exit code $LastExitCode"
    }

    Compress-Archive -Path "$outputDir\unicorn" -DestinationPath "$outputDir\unicorn.zip" -CompressionLevel Optimal
}

if ($IsCi) {
    # TODO: Upload all log files as artifacts
}