#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["LCode.RegistroEventos.ServicioProcesador/LCode.RegistroEventos.ServicioProcesador.csproj", "LCode.RegistroEventos.ServicioProcesador/"]
RUN dotnet restore "LCode.RegistroEventos.ServicioProcesador/LCode.RegistroEventos.ServicioProcesador.csproj"
COPY . .
WORKDIR "/src/LCode.RegistroEventos.ServicioProcesador"
RUN dotnet build "LCode.RegistroEventos.ServicioProcesador.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LCode.RegistroEventos.ServicioProcesador.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LCode.RegistroEventos.ServicioProcesador.dll"]