param (

    [switch] $IsAppVeyor,
    [switch] $IsAzureDevops,
    [Parameter(Mandatory)] [string] $DownloadBase, # The location where WDPs, ZIPs, license.xml are stored    
    [Parameter(Mandatory)] [string] $SQLServer, # The DNS name or IP of the SQL Instance.
    [Parameter(Mandatory)] [string] $SqlAdminUser, # A SQL user with sysadmin privileges.
    [Parameter(Mandatory)] [string] $SqlAdminPassword # The password for $SQLAdminUser.    
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version 2.0

& $PSScriptRoot\build.ps1 -Prefix "helixbase" `
            -Parameters @{SitecoreSiteName="demo.helixbase"} `
            -DownloadBase $DownloadBase `
            -SitecoreVersion "910XP0" `
            -DoBuildSolution `
            -msbuild "msbuild.exe" `
            -vstest "vstest.console.exe" `
            -Configuration "Release" `
            -DoUnitTests `
            -DoInstallPrerequisites `
            -DoInstallSitecore `
            -DoDeploySolution `
            -DoSyncUnicorn `
            -DoSitecorePublish `
            -DoRebuildLinkDatabases:$false `
            -DoRebuildSearchIndexes:$false `
            -DoDeployMarketingDefinitions:$false `
            -DoIntegrationTests `
            -DoFunctionalTests `
            -DoCreateArtifacts `
            -IsAppVeyor:$IsAppVeyor `
            -IsAzureDevops:$IsAzureDevops `
            -SQLServer $SQLServer `
            -SqlAdminUser $SqlAdminUser `
            -SqlAdminPassword $SqlAdminPassword