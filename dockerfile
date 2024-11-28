
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build


WORKDIR /src


COPY . .

# agregando comentario para probar  activar el flujo de CI/CD configurado en el archivo YAML
RUN dotnet restore

# Publica la aplicación en modo Release en la carpeta /app
RUN dotnet publish -c Release -o /app

# Etapa 2: Crear la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Establece el directorio de trabajo en /app
WORKDIR /app

# Exponer el puerto para la aplicación
EXPOSE 5289

# Copia los archivos compilados desde la etapa de construcción
COPY --from=build /app .

# Establece la variable de entorno para la URL de la aplicación
ENV ASPNETCORE_URLS=http://+:5289

# Definir el punto de entrada para la aplicación
ENTRYPOINT ["dotnet", "apiFestivos.Presentacion.dll"]



















