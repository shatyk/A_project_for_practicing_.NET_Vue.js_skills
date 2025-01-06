FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend/Backend.csproj", "Backend/"]
COPY ["Common.Models/Common.Models.csproj", "Common.Models/"]
COPY ["Database/Database.csproj", "Database/"]
RUN dotnet restore "Backend/Backend.csproj"
#RUN dotnet restore "Common.Models/Common.Models.csproj"
#RUN dotnet restore "Database/Database.csproj"
COPY . .
WORKDIR "/src/Backend"
RUN dotnet build "Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
