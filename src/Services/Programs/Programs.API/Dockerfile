#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 56662
EXPOSE 44302

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Programs/Programs.API/Programs.API.csproj", "src/Services/Programs/Programs.API/"]
COPY ["src/BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "src/BuildingBlocks/IntegrationEventLogEF/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "src/BuildingBlocks/EventBusServiceBus/"]
COPY ["src/Services/Programs/Programs.Domain/Programs.Domain.csproj", "src/Services/Programs/Programs.Domain/"]
COPY ["src/Services/Programs/Programs.Infrastructure/Programs.Infrastructure.csproj", "src/Services/Programs/Programs.Infrastructure/"]
RUN dotnet restore "src/Services/Programs/Programs.API/Programs.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Programs/Programs.API"
RUN dotnet build "Programs.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Programs.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ezloyalty.Services.Programs.API.dll"]