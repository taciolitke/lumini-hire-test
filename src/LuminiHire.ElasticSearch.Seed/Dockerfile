FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /app

COPY ["./src/LuminiHire.Domain/LuminiHire.Domain.csproj", "src/LuminiHire.Domain/"]
COPY ["./src/LuminiHire.Infra/LuminiHire.Infra.csproj", "src/LuminiHire.Infra/"]
COPY ["./src/LuminiHire.ElasticSearch.Seed/LuminiHire.ElasticSearch.Seed.csproj", "src/LuminiHire.ElasticSearch.Seed/"]

WORKDIR "/app"

RUN dotnet restore "src/LuminiHire.ElasticSearch.Seed/LuminiHire.ElasticSearch.Seed.csproj"

COPY . .

#RUN dotnet run --project "src/LuminiHire.ElasticSearch.Seed/LuminiHire.ElasticSearch.Seed.csproj"

WORKDIR /app/src/LuminiHire.ElasticSearch.Seed

RUN dotnet build "LuminiHire.ElasticSearch.Seed.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LuminiHire.ElasticSearch.Seed.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LuminiHire.ElasticSearch.Seed.dll"]