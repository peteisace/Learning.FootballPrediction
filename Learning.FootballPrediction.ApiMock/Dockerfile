# build inside a container thatwill; then make a container :/
# sudo docker run -rm 
# -v $(pwd):/app // make currentdir actually ~/app in the build container
# -w /app // workingDir in build container is /app
# mcr.microsoft.com/dotnet/core/sdk:3.1 dotnet publish -c release -o out // container to run, and the command to run in it i.e. compile Release into outdir. we should have content in /app/out if we are succesful. 

# FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
# WORKDIR /app

# # Copy proj file and restore
# COPY *.csproj ./
# RUN dotnet restore

# # Move and build
# COPY . ./
# RUN dotnet publish -c release -o out

# # Build image
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
# WORKDIR /app
# COPY --from=build-env /app/out ./
# ENTRYPOINT [ "/app/Learning.FootballPrediction.ApiMock", "4990" ]
FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY bin/release/net5.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Learning.FootballPrediction.ApiMock.dll"]
#ENTRYPOINT ["dotnet", "Learning.Football.Prediction.ApiMock.dll"]