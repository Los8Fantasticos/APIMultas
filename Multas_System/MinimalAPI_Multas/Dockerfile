#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MinimalAPI_Multas/MinimalAPI_Multas.csproj", "MinimalAPI_Multas/"]
RUN dotnet restore "MinimalAPI_Multas/MinimalAPI_Multas.csproj"
COPY . .
WORKDIR "/src/MinimalAPI_Multas"
RUN dotnet build "MinimalAPI_Multas.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalAPI_Multas.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalAPI_Multas.dll"]