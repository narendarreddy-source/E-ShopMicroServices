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

COPY ["EShop.OrderService.API/EShop.OrderService.API.csproj", "EShop.OrderService.API/"]
COPY ["EShop.OrderService.Application/EShop.OrderService.Application.csproj", "EShop.OrderService.Application/"]
COPY ["EShop.OrderService.Domain/EShop.OrderService.Domain.csproj", "EShop.OrderService.Domain/"]
COPY ["EShop.OrderService.Infrastructure/EShop.OrderService.Infrastructure.csproj", "EShop.OrderService.Infrastructure/"]

RUN dotnet restore "EShop.OrderService.API/EShop.OrderService.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EShop.OrderService.API/EShop.OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EShop.OrderService.API/EShop.OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.OrderService.API.dll"]