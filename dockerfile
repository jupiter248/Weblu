FROM mcr.microsoft.com/dotnet/aspnet:9.0.10 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0.306 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY  ["src/Weblu.Api/Weblu.Api.csproj", "Weblu.Api/"]
COPY  ["src/Weblu.Application/Weblu.Application.csproj", "Weblu.Application/"]
COPY  ["src/Weblu.Domain/Weblu.Domain.csproj", "Weblu.Domain/"]
COPY  ["src/Weblu.Infrastructure/Weblu.Infrastructure.csproj", "Weblu.Infrastructure/"]
RUN dotnet restore "Weblu.Api/Weblu.Api.csproj"
COPY src/ .     


WORKDIR "/src/Weblu.Api"
RUN dotnet build "Weblu.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Weblu.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "Weblu.Api.dll"]