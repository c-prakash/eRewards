#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 56660
EXPOSE 44300

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Accounts/Accounts.API/Accounts.API.csproj", "src/Services/Accounts/Accounts.API/"]
COPY ["src/BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "src/BuildingBlocks/IntegrationEventLogEF/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/Services/Accounts/Accounts.Domain/Accounts.Domain.csproj", "src/Services/Accounts/Accounts.Domain/"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "src/BuildingBlocks/EventBusServiceBus/"]
COPY ["src/Services/Accounts/Accounts.Infrastructure/Accounts.Infrastructure.csproj", "src/Services/Accounts/Accounts.Infrastructure/"]
RUN dotnet restore "src/Services/Accounts/Accounts.API/Accounts.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Accounts/Accounts.API"
RUN dotnet build "Accounts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ezLoyalty.Services.Accounts.API.dll"]