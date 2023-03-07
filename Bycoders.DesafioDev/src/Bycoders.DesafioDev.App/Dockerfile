FROM mcr.microsoft.com/dotnet/nightly/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Bycoders.DesafioDev.App/Bycoders.DesafioDev.App.csproj", "web/Bycoders.DesafioDev.App/"]
RUN dotnet restore "web/Bycoders.DesafioDev.App/Bycoders.DesafioDev.App.csproj"
COPY . .
WORKDIR "Bycoders.DesafioDev.App/"
RUN dotnet build "Bycoders.DesafioDev.App.csproj" -c Release -o /app/build

FROM build AS publish
Run dotnet publish "Bycoders.DesafioDev.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","Bycoders.DesafioDev.App.dll"]

