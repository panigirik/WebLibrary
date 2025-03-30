FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5228  


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebLibrary/WebLibrary.csproj", "WebLibrary/"]
COPY ["WebLibrary.Persistance/WebLibrary.Persistance.csproj", "WebLibrary.Persistance/"]
COPY ["WebLibrary.Domain/WebLibrary.Domain.csproj", "WebLibrary.Domain/"]
COPY ["WebLibrary.BackgroundService/WebLibrary.BackgroundService.csproj", "WebLibrary.BackgroundService/"]
COPY ["WebLibrary.Application/WebLibrary.Application.csproj", "WebLibrary.Application/"]
COPY ["WebLibrary.Indentity/WebLibrary.Indentity.csproj", "WebLibrary.Indentity/"]
COPY ["WebLibrary.ValidationServices/WebLibrary.ValidationServices.csproj", "WebLibrary.ValidationServices/"]
RUN dotnet restore "WebLibrary/WebLibrary.csproj"
COPY . .
WORKDIR "/src/WebLibrary"
RUN dotnet build "WebLibrary.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebLibrary.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebLibrary.dll"]
