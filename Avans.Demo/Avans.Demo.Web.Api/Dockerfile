#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Avans.Demo.Web.Api/Avans.Demo.Web.Api.csproj", "Avans.Demo.Web.Api/"]
COPY ["Avans.Demo.DataAccess.SqlLite/Avans.Demo.DataAccess.SqlLite.csproj", "Avans.Demo.DataAccess.SqlLite/"]
COPY ["Avans.Demo.DataAccess/Avans.Demo.DataAccess.csproj", "Avans.Demo.DataAccess/"]
COPY ["Avans.Demo.Logic/Avans.Demo.Logic.csproj", "Avans.Demo.Logic/"]
COPY ["Avans.Demo.Domain/Avans.Demo.Domain.csproj", "Avans.Demo.Domain/"]
RUN dotnet restore "Avans.Demo.Web.Api/Avans.Demo.Web.Api.csproj"
COPY . .
WORKDIR "/src/Avans.Demo.Web.Api"
RUN dotnet build "Avans.Demo.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Avans.Demo.Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Avans.Demo.Web.Api.dll"]