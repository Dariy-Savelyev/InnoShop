FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN useradd -ms /bin/bash appuser
USER appuser
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserService/InnoShop.UserService.WebApi/*.csproj", "UserService/InnoShop.UserService.WebApi/"]
COPY ["UserService/InnoShop.UserService.Application/*.csproj", "UserService/InnoShop.UserService.Application/"]
COPY ["UserService/InnoShop.UserService.Domain/*.csproj", "UserService/InnoShop.UserService.Domain/"]
COPY ["UserService/InnoShop.UserService.CrossCutting/*.csproj", "UserService/InnoShop.UserService.CrossCutting/"]
COPY ["UserService/InnoShop.UserService.Container/*.csproj", "UserService/InnoShop.UserService.Container/"]
COPY ["InnoShop.GatewayService/*.csproj", "InnoShop.GatewayService/"]
RUN dotnet restore "UserService/InnoShop.UserService.WebApi/UserService.WebApi.csproj"

COPY . .
WORKDIR "/src/UserService/InnoShop.UserService.WebApi"
RUN dotnet build "UserService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.WebApi.dll"]