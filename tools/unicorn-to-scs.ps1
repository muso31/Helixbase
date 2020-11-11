############################################################
#   Converts Unicorn configurations to                     #
#   Sitecore Serialization Json Configs                    #
############################################################

Function Invoke-ReadUnicornConfig {
    param(
        [Parameter(Mandatory=$true)]
        [string]$ProjectPath,
        [Parameter(Mandatory=$true)]
        [string]$NewTargetFramework    
    )
}


Function Invoke-RenameSerializationFolder {
    param(
        [Parameter(Mandatory=$true)]
        [string]$ItemsDirectory,
        [Parameter(Mandatory=$true)]
        [string]$StringToRemove    
    )

    Get-ChildItem -Path $ItemsDirectory -Directory -ErrorAction SilentlyContinue | ForEach-Object {
        Rename-Item -Path $_.FullName -NewName $_.FullName.Replace($StringToRemove,"") -ErrorAction SilentlyContinue
    } 


    #Get-ChildItem -Path $ItemsDirectory -Directory | Rename-Item $_ $_.Replace($StringToRemove,'')
}

Invoke-RenameSerializationFolder `
    -ItemsDirectory "C:\Projects\Ethisys\Helixbase\src\Feature\VersionTrim\items" `
    -StringToRemove "Feature.VersionTrim."