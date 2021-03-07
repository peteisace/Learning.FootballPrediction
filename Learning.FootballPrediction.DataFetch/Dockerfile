FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy proj file and restore
COPY *.csproj ./
RUN dotnet restore

# Move and build
COPY . ./
RUN dotnet publish -c release -o out

# Build image
FROM mcr.microsoft.co,m/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "Learning.FootballPrediction.DataFetch"]