#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Points/Points.API/Points.API.csproj", "src/Services/Points/Points.API/"]
RUN dotnet restore "src/Services/Points/Points.API/Points.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Points/Points.API"
RUN dotnet build "Points.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Points.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Points.API.dll"]