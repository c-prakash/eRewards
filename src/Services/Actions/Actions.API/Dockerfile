#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 56664
EXPOSE 44304

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Actions/Actions.API/Actions.API.csproj", "src/Services/Actions/Actions.API/"]
COPY ["src/BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "src/BuildingBlocks/IntegrationEventLogEF/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/Services/Actions/Actions.Infrastructure/Actions.Infrastructure.csproj", "src/Services/Actions/Actions.Infrastructure/"]
COPY ["src/Services/Actions/Actions.Domain/Actions.Domain.csproj", "src/Services/Actions/Actions.Domain/"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "src/BuildingBlocks/EventBusServiceBus/"]
RUN dotnet restore "src/Services/Actions/Actions.API/Actions.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Actions/Actions.API"
RUN dotnet build "Actions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actions.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ezLoyalty.Services.Actions.API.dll"]