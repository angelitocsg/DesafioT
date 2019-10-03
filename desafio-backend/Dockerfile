FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DesafioT.API/DesafioT.API.csproj", "DesafioT.API/"]
COPY ["DesafioT.Domain/DesafioT.Domain.csproj", "DesafioT.Domain/"]
COPY ["DesafioT.Infra.Data/DesafioT.Infra.Data.csproj", "DesafioT.Infra.Data/"]
COPY ["DesafioT.Tests/DesafioT.Tests.csproj", "DesafioT.Tests/"]

RUN dotnet restore "DesafioT.API/DesafioT.API.csproj"

COPY . .

RUN dotnet test
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /property:PublishWithAspNetCoreTargetManifest=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY DesafioT.Infra.Data/Data/ ./data/

ENV ASPNETCORE_URLS="http://+:5000"

ENTRYPOINT ["dotnet", "DesafioT.API.dll"]