#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Web/Web.csproj", "src/Web/"]
COPY ["src/Desafio.CrossCutting.IoC/Desafio.CrossCutting.IoC.csproj", "src/Desafio.CrossCutting.IoC/"]
COPY ["src/Desafio.Core.Domain/Desafio.Core.Domain.csproj", "src/Desafio.Core.Domain/"]
COPY ["src/Desafio.Tests/Desafio.Tests.csproj", "src/Desafio.Tests/"]
COPY ["src/Desafio.Infra/Desafio.Infra.csproj", "src/Desafio.Infra/"]
RUN dotnet restore "src/Web/Web.csproj"
COPY . .
WORKDIR "/src/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]
