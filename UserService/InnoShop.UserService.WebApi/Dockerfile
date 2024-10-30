FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN useradd -ms /bin/bash appuser
USER appuser
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./ .
RUN dotnet restore "./UserService/InnoShop.UserService.WebApi/InnoShop.UserService.WebApi.csproj"
WORKDIR "/src/UserService/InnoShop.UserService.WebApi"
RUN dotnet build "./InnoShop.UserService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InnoShop.UserService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoShop.UserService.WebApi.dll"]