#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WorkerTracking.Api/WorkerTracking.Api.csproj", "WorkerTracking.Api/"]
COPY ["WorkerTracking.Data/WorkerTracking.Data.csproj", "WorkerTracking.Data/"]
COPY ["WorkerTracking.Entities/WorkerTracking.Entities.csproj", "WorkerTracking.Entities/"]
COPY ["WorkerTracking.Core/WorkerTracking.Core.csproj", "WorkerTracking.Core/"]
RUN dotnet restore "WorkerTracking.Api/WorkerTracking.Api.csproj"
COPY . .
WORKDIR "/src/WorkerTracking.Api"
RUN dotnet build "WorkerTracking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerTracking.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerTracking.Api.dll"]