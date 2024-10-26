FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN useradd -ms /bin/bash appuser
USER appuser
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProductService/InnoShop.ProductService.WebApi/*.csproj", "ProductService/InnoShop.ProductService.WebApi/"]
COPY ["ProductService/InnoShop.ProductService.Application/*.csproj", "ProductService/InnoShop.ProductService.Application/"]
COPY ["ProductService/InnoShop.ProductService.Domain/*.csproj", "ProductService/InnoShop.ProductService.Domain/"]
COPY ["ProductService/InnoShop.ProductService.CrossCutting/*.csproj", "ProductService/InnoShop.ProductService.CrossCutting/"]
COPY ["ProductService/InnoShop.ProductService.Container/*.csproj", "ProductService/InnoShop.ProductService.Container/"]
COPY ["InnoShop.GatewayService/*.csproj", "InnoShop.GatewayService/"]
RUN dotnet restore "ProductService/InnoShop.ProductService.WebApi/ProductService.WebApi.csproj"

COPY . .
WORKDIR "/src/ProductService/InnoShop.ProductService.WebApi"
RUN dotnet build "ProductService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProductService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.WebApi.dll"]