# escape=`

ARG PARENT_IMAGE
ARG SOLUTION_IMAGE
ARG MANAGEMENT_SERVICES_IMAGE

FROM ${SOLUTION_IMAGE} as solution
FROM ${MANAGEMENT_SERVICES_IMAGE} AS management_services
FROM ${PARENT_IMAGE}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

WORKDIR C:\inetpub\wwwroot

# Copy the Sitecore Management Services Module
COPY --from=management_services C:\module\cm\content C:\inetpub\wwwroot
# Copy solution website files
COPY --from=solution /artifacts/sitecore/ ./

HEALTHCHECK --interval=5s `
 CMD powershell -command `
    try { `
     $response = iwr http://localhost/healthz/ready -UseBasicParsing; `
     if ($response.StatusCode -eq 200) { return 0} `
     else {return 1}; `
    } catch { return 1 }