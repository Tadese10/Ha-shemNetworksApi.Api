FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Ha-shemNetworksApi.Api/Ha-shemNetworksApi.Api.csproj", "Ha-shemNetworksApi.Api/"]
RUN dotnet restore "Ha-shemNetworksApi.Api/Ha-shemNetworksApi.Api.csproj"
COPY . .
WORKDIR "/src/Ha-shemNetworksApi.Api"
RUN dotnet build "Ha-shemNetworksApi.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Ha-shemNetworksApi.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ha-shemNetworksApi.Api.dll"]