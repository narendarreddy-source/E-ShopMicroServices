# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["EShop.CatalogService.API/EShop.CatalogService.API.csproj", "EShop.CatalogService.API/"]
COPY ["EShop.CatalogService.Application/EShop.CatalogService.Application.csproj", "EShop.CatalogService.Application/"]
COPY ["EShop.CatalogService.Domain/EShop.CatalogService.Domain.csproj", "EShop.CatalogService.Domain/"]
COPY ["EShop.CatalogService.Infrastructure/EShop.CatalogService.Infrastructure.csproj", "EShop.CatalogService.Infrastructure/"]
COPY ["Eshop.Shared/Eshop.Shared.csproj", "Eshop.Shared/"]

RUN dotnet restore "EShop.CatalogService.API/EShop.CatalogService.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EShop.CatalogService.API/EShop.CatalogService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EShop.CatalogService.API/EShop.CatalogService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.CatalogService.API.dll"]