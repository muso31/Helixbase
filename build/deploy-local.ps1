Set-StrictMode -Version 2.0
$ErrorActionPreference = "Stop"

& $PSScriptRoot\build.ps1 -Prefix "helixbase" `
            -Parameters @{SitecoreSiteName="demo.helixbase"} `
            -DownloadBase "https://sitecoredevops.blob.core.windows.net/installs" `
            -SitecoreVersion "910XP0" `
            -DoBuildSolution `
            -Configuration "Debug" `
            -DoInstallSitecore `
            -DoDeploySolution `
            -DoSyncUnicorn `
            -DoSitecorePublish `
            -DoRebuildLinkDatabases `
            -DoRebuildSearchIndexes `
            -DoDeployMarketingDefinitions `
            -SqlServer . `
            -SqlAdminUser sa `
            -SqlAdminPassword '12345' `
            -SitecoreAdminPassword b