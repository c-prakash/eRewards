#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 56666
EXPOSE 44306

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Incentive/Incentive.API/Incentive.API.csproj", "src/Services/Incentive/Incentive.API/"]
COPY ["src/Services/Incentive/Incentive.Infrastructure/Incentive.Infrastructure.csproj", "src/Services/Incentive/Incentive.Infrastructure/"]
COPY ["src/Services/Incentive/Incentive.Domain/Incentive.Domain.csproj", "src/Services/Incentive/Incentive.Domain/"]
COPY ["src/BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "src/BuildingBlocks/IntegrationEventLogEF/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "src/BuildingBlocks/EventBusServiceBus/"]
RUN dotnet restore "src/Services/Incentive/Incentive.API/Incentive.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Incentive/Incentive.API"
RUN dotnet build "Incentive.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Incentive.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ezLoyalty.Services.Incentive.API.dll"]