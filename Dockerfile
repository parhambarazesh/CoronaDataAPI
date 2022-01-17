#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src/CoronaDataApi
COPY ["CoronaDataApi/CoronaDataApi.csproj", "."]
RUN dotnet restore
COPY ./CoronaDataApi/. .
RUN dotnet publish -c Release -o /app/build/api

FROM base AS final
WORKDIR /app
COPY --from=build /app/build/api .
ENTRYPOINT ["dotnet", "CoronaDataApi.dll"]