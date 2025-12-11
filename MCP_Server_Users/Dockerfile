# -------------------------------------------------------------------------
# ETAPA 1: BUILD (Compilación)
FROM mcr.microsoft.com/dotnet/sdk:10.0-windowsservercore-ltsc2022 AS build
WORKDIR /src

# Copiar el archivo .csproj y restaurar dependencias
COPY ["MCP_Server_Users.csproj", "./"]
RUN dotnet restore "MCP_Server_Users.csproj"

# Copiar el resto del código fuente.
COPY . .

# Publicar la aplicación como self-contained y single-file.
RUN dotnet publish "MCP_Server_Users.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o /app/publish

# -------------------------------------------------------------------------
# ETAPA 2: FINAL (Runtime)
FROM mcr.microsoft.com/windows/servercore:ltsc2022 AS final
WORKDIR /app

# Copiar el ejecutable
COPY --from=build /app/publish .

# *** Solución de RED (ASPNETCORE_URLS): Forzar a la aplicación a escuchar en 0.0.0.0 ***
ENV ASPNETCORE_URLS=http://0.0.0.0:3040

# La aplicación escucha en el puerto 3040
EXPOSE 3040

# *** CORRECCIÓN FINAL: Usar CMD para ejecutar el .exe de forma confiable ***
ENTRYPOINT ["cmd", "/C", "MCP_Server_Users.exe"]