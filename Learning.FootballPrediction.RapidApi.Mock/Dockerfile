FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY bin/Release/net5.0/publish App/
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /App
EXPOSE 4992
ENTRYPOINT [ "dotnet", "Learning.FootballPrediction.RapidApi.Mock.dll" ]