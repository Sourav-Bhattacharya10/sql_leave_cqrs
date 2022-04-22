# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY API/*.csproj /source/API/
COPY Application/*.csproj /source/Application/
COPY Domain/*.csproj /source/Domain/
COPY Infrastructure/*.csproj /source/Infrastructure/
COPY Persistence/*.csproj /source/Persistence/
RUN dotnet restore /source/API/API.csproj

# copy and build app and libraries
COPY API/ /source/API/
COPY Application/ /source/Application/
COPY Domain/ /source/Domain/
COPY Infrastructure/ /source/Infrastructure/
COPY Persistence/ /source/Persistence/
WORKDIR /source/API
RUN dotnet build -c release --no-restore

# test stage -- exposes optional entrypoint
# target entrypoint with: docker build --target test
# FROM build AS test
# WORKDIR /source/tests
# COPY tests/ .
# ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM build AS publish
RUN dotnet publish -c release --no-build -o /app --no-cache

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0.4-focal
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API.dll"]
