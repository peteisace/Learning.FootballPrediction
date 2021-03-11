FROM mcr.microsoft.com/dotnet/runtime:5.0
COPY bin/release/net5.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "Learning.FootballPrediction.DataFetch.dll"]