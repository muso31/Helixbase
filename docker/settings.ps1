$rootHostDomain = "helixbase.localhost"

Function Invoke-ContainerLogin {
    # Login to Registry
    az login
    az acr login --name "ethisys"
}