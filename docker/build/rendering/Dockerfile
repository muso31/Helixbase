# escape=`

# This is an example Dockerfile for an ASP.NET Core Rendering Host.
# We use build stages to enable 'dotnet watch' during development, so
# that changes to your rendering code can be quickly tested, including in
# the Experience Editor. Be sure to watch the container logs in case of build errors.

ARG DEBUG_PARENT_IMAGE
ARG SOLUTION_IMAGE

FROM ${DEBUG_PARENT_IMAGE} as debug
WORKDIR /solution/src
EXPOSE 80
ENTRYPOINT ["dotnet", "watch", "-v", "--project", ".\\rendering", "run", "--no-launch-profile"]

FROM ${SOLUTION_IMAGE} as solution
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as release
WORKDIR /app
COPY --from=solution /artifacts/rendering/ ./
EXPOSE 80
ENTRYPOINT ["dotnet", "EthisysHeadless.dll"]