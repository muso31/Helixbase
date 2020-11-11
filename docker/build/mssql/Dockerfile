# escape=`

ARG BASE_IMAGE
ARG HEADLESS_SERVICES_IMAGE

FROM ${HEADLESS_SERVICES_IMAGE} AS headless_services
FROM ${BASE_IMAGE}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Copy and init the JSS / Headless Services Module
COPY --from=headless_services C:\module\db C:\jss_data
RUN C:\DeployDatabases.ps1 -ResourcesDirectory C:\jss_data; `
    Remove-Item -Path C:\jss_data -Recurse -Force;