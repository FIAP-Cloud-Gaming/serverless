FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY nuget.config .
COPY src/Functions/Functions.csproj src/Functions/

RUN dotnet restore src/Functions/Functions.csproj

COPY . .

RUN dotnet publish src/Functions/Functions.csproj \
    -c $BUILD_CONFIGURATION \
    --no-restore \
    -o /app/publish \
    /p:UseAppHost=false

# ------------------------------------------------------------------------------

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated10.0 AS final

LABEL org.opencontainers.image.title="FCG Functions" \
      org.opencontainers.image.description="Azure Functions serverless - FIAP CloudGaming" \
      org.opencontainers.image.source="https://github.com/FIAP-Cloud-Gaming/Serverless" \
      org.opencontainers.image.authors="Alessandro (DEV)"

ENV AzureWebJobsScriptRoot=/home/site/wwwroot

COPY --from=build /app/publish /home/site/wwwroot