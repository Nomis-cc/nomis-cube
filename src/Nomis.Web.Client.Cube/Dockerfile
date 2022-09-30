#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Nomis.Web.Client.Cube/Nomis.Web.Client.Cube.csproj", "src/Nomis.Web.Client.Cube/"]
RUN dotnet restore "src/Nomis.Web.Client.Cube/Nomis.Web.Client.Cube.csproj"
COPY . .
WORKDIR "/src/src/Nomis.Web.Client.Cube"
RUN dotnet build "Nomis.Web.Client.Cube.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nomis.Web.Client.Cube.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nomis.Web.Client.Cube.dll"]