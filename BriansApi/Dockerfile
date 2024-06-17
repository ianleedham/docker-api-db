FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BriansApi/BriansApi.csproj", "BriansApi/"]
RUN dotnet restore "BriansApi/BriansApi.csproj"
COPY . .
WORKDIR "/src/BriansApi"
RUN dotnet build "BriansApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BriansApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

## Run migrations
FROM buildbase as migrations
RUN dotnet tool install --version 6.0.9 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet-ef database update --project src/Infrastructure/ --startup-project src/Api/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BriansApi.dll"]
