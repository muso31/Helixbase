Function Set-PublishUrl {
    param(
        [Parameter(Mandatory)][string] $configPath, 
        [Parameter(Mandatory)][string] $publishUrl
    )
    
    [xml]$config = Get-Content $configPath
    $config.Project.PropertyGroup.PublishUrl = $publishUrl
    $config.Save($configPath)  
}

Function Set-SourceFolder {
    param(
        [Parameter(Mandatory)][string] $configPath,
        [Parameter(Mandatory)][string] $websitePath,
        [Parameter(Mandatory)][string] $sourceFolder
    )
    
    [xml]$config = Get-Content $configPath
    $config.configuration.sitecore.SelectSingleNode("*").value = $sourceFolder

    $targetPath = [IO.Path]::Combine($websitePath, "App_Config", "Environment", "SourceFolder.config")
    $config.Save($targetPath)
}

Function Set-Unicorn-Shared-Secret {
    param(
        [Parameter(Mandatory)][string] $configPath,
        [Parameter(Mandatory)][string] $websitePath,
        [Parameter(Mandatory)][string] $sharedSecret
    )
    
    [xml]$config = Get-Content $configPath
    $config.configuration.sitecore.unicorn.authenticationProvider.SharedSecret = $sharedSecret

    $targetPath = [IO.Path]::Combine($websitePath, "App_Config", "Environment", "Unicorn.SharedSecret.config")
    $config.Save($targetPath)
}