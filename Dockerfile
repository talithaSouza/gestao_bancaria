# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia a solução e os arquivos .csproj de todos os projetos
COPY ./API/API.csproj ./API/
COPY ./Domain/Domain.csproj ./Domain/
COPY ./Infra/Infra.csproj ./Infra/
COPY ./Service/Service.csproj ./Service/
# Adicione outros .csproj se houver (ex: config)

# Restaura os pacotes
RUN dotnet restore "API/API.csproj"

# Copia tudo (agora que já restaurou)
COPY .. .

# Publica o projeto da API
WORKDIR /src/API
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5284

ENTRYPOINT ["dotnet", "API.dll"]
