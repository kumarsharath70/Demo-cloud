#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1433

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /src
COPY ["ASPCoreWithAngular/ASPCoreWithAngular.csproj", "ASPCoreWithAngular/"]
RUN dotnet restore "ASPCoreWithAngular/ASPCoreWithAngular.csproj"
COPY . .
WORKDIR "/src/ASPCoreWithAngular"
RUN dotnet build "ASPCoreWithAngular.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASPCoreWithAngular.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASPCoreWithAngular.dll"]
