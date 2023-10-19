FROM mcr.microsoft.com/dotnet/sdk:6.0 AS AUX
WORKDIR webapp

EXPOSE 80
EXPOSE 5000

#COPIAMOS ARCHIVOS

COPY ./*.csproj ./
RUN dotnet restore #VALIDAMOS QUE CONTENGA TODOS LOS PAQUETES

COPY . .	
RUN dotnet publish -c Release -o salida

#CONSTRUIMOS IMAGEN
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR webapp
COPY --from=AUX /webapp/salida .
ENTRYPOINT ["dotnet", "Reportes.dll"]
