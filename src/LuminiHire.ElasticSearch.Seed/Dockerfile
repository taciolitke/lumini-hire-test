#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/LuminiHire.ElasticSearch.Seed/LuminiHire.ElasticSearch.Seed.csproj", "src/LuminiHire.ElasticSearch.Seed/"]
COPY ["src/LuminiHire.Domain/LuminiHire.Domain.csproj", "src/LuminiHire.Domain/"]
COPY ["src/LuminiHire.Infra/LuminiHire.Infra.csproj", "src/LuminiHire.Infra/"]
RUN dotnet restore "src/LuminiHire.ElasticSearch.Seed/LuminiHire.ElasticSearch.Seed.csproj"
COPY . .
WORKDIR "/src/src/LuminiHire.ElasticSearch.Seed"
RUN dotnet build "LuminiHire.ElasticSearch.Seed.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LuminiHire.ElasticSearch.Seed.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LuminiHire.ElasticSearch.Seed.dll"]