# escape=`

# This is an example Dockerfile for an ASP.NET Core Rendering Host.
# We use build stages to enable 'dotnet watch' during development, so
# that changes to your rendering code can be quickly tested, including in
# the Experience Editor. Be sure to watch the container logs in case of build errors.

ARG DEBUG_BASE_IMAGE
ARG RELEASE_BASE_IMAGE
ARG SOLUTION_IMAGE

FROM ${DEBUG_BASE_IMAGE} as debug

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

WORKDIR C:\solution\src
ENTRYPOINT ["dotnet", "watch", "-v", "--project", ".\\Project\\Helixbase\\rendering", "run", "--no-launch-profile"]

FROM ${SOLUTION_IMAGE} as solution
FROM ${RELEASE_BASE_IMAGE} as release

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

WORKDIR C:\app
COPY --from=solution \artifacts\rendering\ .\
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Helixbase.Project.Helixbase.dll"]