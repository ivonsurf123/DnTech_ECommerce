# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
# ✅ Create the folder and give app user permission before switching users
RUN mkdir -p /home/app/.aspnet/DataProtection-Keys \
    && chown -R $APP_UID /home/app/.aspnet
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DnTech_ECommerce.csproj", "."]
RUN dotnet restore "DnTech_ECommerce.csproj"
COPY . .
# ✅ Stay in /src, no subfolder needed
RUN dotnet build "DnTech_ECommerce.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
# ✅ Same fix here
RUN dotnet publish "DnTech_ECommerce.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DnTech_ECommerce.dll"]
