#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["LCode.RegistroEventos.WebHook/LCode.RegistroEventos.WebHook.csproj", "LCode.RegistroEventos.WebHook/"]
RUN dotnet restore "LCode.RegistroEventos.WebHook/LCode.RegistroEventos.WebHook.csproj"
COPY . .
WORKDIR "/src/LCode.RegistroEventos.WebHook"
RUN dotnet build "LCode.RegistroEventos.WebHook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LCode.RegistroEventos.WebHook.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LCode.RegistroEventos.WebHook.dll"]