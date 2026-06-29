FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5126

ENV ASPNETCORE_URLS=http://+:5126

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["JWTAuthDemo.csproj", "./"]
RUN dotnet restore "JWTAuthDemo.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "JWTAuthDemo.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "JWTAuthDemo.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTAuthDemo.dll"]
