dotnet build ../ --configuration Release
dotnet publish ../ --configuration Release
docker build ..\bin\Release\net6.0\publish -t server:latest -t server:1.0.0