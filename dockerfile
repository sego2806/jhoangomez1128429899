# Etapa 1: Construcci�n de la aplicaci�n
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establece el directorio de trabajo en /src
WORKDIR /src

# Copia los archivos del proyecto en el contenedor
COPY . .

# Restaura las dependencias del proyecto (nuget)
RUN dotnet restore

# Publica la aplicaci�n en modo Release en la carpeta /app
RUN dotnet publish -c Release -o /app

# Etapa 2: Crear la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Establece el directorio de trabajo en /app
WORKDIR /app

# Exponer el puerto para la aplicaci�n
EXPOSE 5289

# Copia los archivos compilados desde la etapa de construcci�n
COPY --from=build /app .

# Establece la variable de entorno para la URL de la aplicaci�n
ENV ASPNETCORE_URLS=http://+:5289

# Definir el punto de entrada para la aplicaci�n
ENTRYPOINT ["dotnet", "apiFestivos.Presentacion.dll"]



















