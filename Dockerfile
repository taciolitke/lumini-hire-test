FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY *.sln .

COPY ["src/LuminiHire/LuminiHire.csproj", "src/LuminiHire/"]
COPY ["src/LuminiHire.Domain/LuminiHire.Domain.csproj", "src/LuminiHire.Domain/"]
COPY ["src/LuminiHire.Infra/LuminiHire.Infra.csproj", "src/LuminiHire.Infra/"]

WORKDIR "/app"

RUN dotnet restore "src/LuminiHire/LuminiHire.csproj"

COPY . .

WORKDIR /app/src/LuminiHire

RUN dotnet build "LuminiHire.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LuminiHire.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LuminiHire.dll"]