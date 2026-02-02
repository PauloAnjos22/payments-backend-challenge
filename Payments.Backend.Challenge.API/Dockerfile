FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Payments.Backend.Challenge.API/Payments.Backend.Challenge.API.csproj", "Payments.Backend.Challenge.API/"]
COPY ["Payments.Backend.Challenge.Application/Payments.Backend.Challenge.Application.csproj", "Payments.Backend.Challenge.Application/"]
COPY ["Payments.Backend.Challenge.Domain/Payments.Backend.Challenge.Domain.csproj", "Payments.Backend.Challenge.Domain/"]
COPY ["Payments.Backend.Challenge.Infrastructure/Payments.Backend.Challenge.Infrastructure.csproj", "Payments.Backend.Challenge.Infrastructure/"]
RUN dotnet restore "Payments.Backend.Challenge.API/Payments.Backend.Challenge.API.csproj"
COPY . .
WORKDIR "/src/Payments.Backend.Challenge.API"
RUN dotnet build "./Payments.Backend.Challenge.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Payments.Backend.Challenge.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payments.Backend.Challenge.API.dll"]
