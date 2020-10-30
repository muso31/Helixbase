param(
    [Parameter(Mandatory = $false)][switch] $DeleteImages
)

$registryName = "ethisys.azurecr.io/"


Get-ChildItem -Path (Join-Path $PSScriptRoot "\data") -Directory | ForEach-Object {
    $dataPath = $_.FullName

    Get-ChildItem -Path $dataPath -Exclude ".gitkeep" -Recurse | Remove-Item -Force -Recurse -Verbose
}

Get-ChildItem -Path (Join-Path $PSScriptRoot "\deploy") -Directory | ForEach-Object {
    $deployPath = $_.FullName

    Get-ChildItem -Path $deployPath -Exclude ".gitkeep" -Recurse | Remove-Item -Force -Recurse -Verbose
}

if($DeleteImages) {
    # docker inspect --format='{{.Id}} {{.Parent}}' $(docker images --filter=reference="$registryName/helixbase*" since=<image_id> -q)
    # docker rmi {sub_image_id} 
   
    docker rmi (docker images -q ethisys.azurecr.io/helixbase-*) -f
    
    #docker inspect --format='{{.Id}} {{.Parent}}' $(docker images --quiet -q ethisys.azurecr.io/helixbase-*) 
}
