FROM mcr.microsoft.com/dotnet/nightly/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Bycoders.DesafioDev.API/Bycoders.DesafioDev.API.csproj", "api/Bycoders.DesafioDev.API/"]
RUN dotnet restore "api/Bycoders.DesafioDev.API/Bycoders.DesafioDev.API.csproj"
COPY . .
WORKDIR "Bycoders.DesafioDev.API/"
RUN dotnet build "Bycoders.DesafioDev.API.csproj" -c Release -o /app/build

FROM build AS publish
Run dotnet publish "Bycoders.DesafioDev.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","Bycoders.DesafioDev.API.dll"]

