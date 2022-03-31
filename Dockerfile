FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENTRYPOINT ["dotnet", "Server.dll"]
ARG source=.
WORKDIR /app
EXPOSE 22000
EXPOSE 80
COPY $source .