#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Rewards/Services/Transactions/Transactions.API/Transactions.API.csproj", "src/Rewards/Services/Transactions/Transactions.API/"]
COPY ["src/Rewards/Services/Transactions/Transactions.Domain/Transactions.Domain.csproj", "src/Rewards/Services/Transactions/Transactions.Domain/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/Rewards/Services/Transactions/Transactions.Infrastructure/Transactions.Infrastructure.csproj", "src/Rewards/Services/Transactions/Transactions.Infrastructure/"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "src/BuildingBlocks/EventBusServiceBus/"]
RUN dotnet restore "src/Rewards/Services/Transactions/Transactions.API/Transactions.API.csproj"
COPY . .
WORKDIR "/src/src/Rewards/Services/Transactions/Transactions.API"
RUN dotnet build "Transactions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transactions.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eRewards.Services.Transactions.API.dll"]