#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/PS.Cart/PS.Cart.csproj", "src/Services/PS.Cart/"]
COPY ["src/Building Blocks/PS.MessageBus/PS.MessageBus.csproj", "src/Building Blocks/PS.MessageBus/"]
COPY ["src/Building Blocks/PS.Core/PS.Core.csproj", "src/Building Blocks/PS.Core/"]
COPY ["src/Building Blocks/PS.WebApi.Core/PS.WebApi.Core.csproj", "src/Building Blocks/PS.WebApi.Core/"]
RUN dotnet restore "src/Services/PS.Cart/PS.Cart.csproj"
COPY . .
WORKDIR "/src/src/Services/PS.Cart"
RUN dotnet build "PS.Cart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PS.Cart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PS.Cart.dll"]