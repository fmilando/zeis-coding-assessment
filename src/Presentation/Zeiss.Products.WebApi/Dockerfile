FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5056
EXPOSE 7192

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Zeiss.Products.WebApi/Zeiss.Products.WebApi.csproj", "src/Presentation/Zeiss.Products.WebApi/"]
COPY ["src/Core/Zeiss.Products.Application/Zeiss.Products.Application.csproj", "src/Core/Zeiss.Products.Application/"]
COPY ["src/Core/Zeiss.Products.Domain/Zeiss.Products.Domain.csproj", "src/Core/Zeiss.Products.Domain/"]
COPY ["src/Infrastructure/Zeiss.Products.Infrastructure/Zeiss.Products.Infrastructure.csproj", "src/Infrastructure/Zeiss.Products.Infrastructure/"]
RUN dotnet restore "src/Presentation/Zeiss.Products.WebApi/Zeiss.Products.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Zeiss.Products.WebApi"
RUN dotnet build "./Zeiss.Products.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Zeiss.Products.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zeiss.Products.WebApi.dll"]
