#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiGateways/ShoppingCartAggregator/ShoppingCartAggregator.csproj", "ApiGateways/ShoppingCartAggregator/"]
RUN dotnet restore "ApiGateways/ShoppingCartAggregator/ShoppingCartAggregator.csproj"
COPY . .
WORKDIR "/src/ApiGateways/ShoppingCartAggregator"
RUN dotnet build "ShoppingCartAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingCartAggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCartAggregator.dll"]
