#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/ezloyalty/Services/Actions/Actions.API/Actions.API.csproj", "src/ezloyalty/Services/Actions/Actions.API/"]
COPY ["src/ezloyalty/Services/Actions/Actions.Domain/Actions.Domain.csproj", "src/ezloyalty/Services/Actions/Actions.Domain/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/ezloyalty/Services/Actions/Actions.Infrastructure/Actions.Infrastructure.csproj", "src/ezloyalty/Services/Actions/Actions.Infrastructure/"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "src/BuildingBlocks/EventBusServiceBus/"]
RUN dotnet restore "src/ezloyalty/Services/Actions/Actions.API/Actions.API.csproj"
COPY . .
WORKDIR "/src/src/ezloyalty/Services/Actions/Actions.API"
RUN dotnet build "Actions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actions.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ezloyalty.Services.Actions.API.dll"]