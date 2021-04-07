FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 44351

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["films.api/films.api.csproj", "films.api/"]
RUN dotnet restore "films.api/films.api.csproj"
COPY . .
WORKDIR "/src/films.api"
RUN dotnet build "films.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "films.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "films.api.dll"]
