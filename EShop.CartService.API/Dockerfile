# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EShop.CartService.API/EShop.CartService.API.csproj", "EShop.CartService.API/"]
COPY ["EShop.CartService.Application/EShop.CartService.Application.csproj", "EShop.CartService.Application/"]
COPY ["EShop.CartService.Infrastructure/EShop.CartService.Infrastructure.csproj", "EShop.CartService.Infrastructure/"]
COPY ["EShop.CartService.Domain/EShop.CartService.Domain.csproj", "EShop.CartService.Domain/"]
COPY ["Eshop.Shared/Eshop.Shared.csproj", "Eshop.Shared/"]
RUN dotnet restore "./EShop.CartService.API/EShop.CartService.API.csproj"
COPY . .
WORKDIR "/src/EShop.CartService.API"
RUN dotnet build "./EShop.CartService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EShop.CartService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.CartService.API.dll"]