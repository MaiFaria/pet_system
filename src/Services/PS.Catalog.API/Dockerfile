#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/PS.Catalog.API/PS.Catalog.API.csproj", "src/Services/PS.Catalog.API/"]
COPY ["src/Building Blocks/PS.MessageBus/PS.MessageBus.csproj", "src/Building Blocks/PS.MessageBus/"]
COPY ["src/Building Blocks/PS.Core/PS.Core.csproj", "src/Building Blocks/PS.Core/"]
COPY ["src/Building Blocks/PS.WebApi.Core/PS.WebApi.Core.csproj", "src/Building Blocks/PS.WebApi.Core/"]
RUN dotnet restore "src/Services/PS.Catalog.API/PS.Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/Services/PS.Catalog.API"
RUN dotnet build "PS.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PS.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PS.Catalog.API.dll"]