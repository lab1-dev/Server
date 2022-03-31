# Server
.Net Server Backend - Optional Asp.net Server project for Lab1.

This project provides a fast web server for Lab1 applications. It is an alternative option for providing Lab1 SPA applications. 

# Build Instructions

1. Download [.Net 6 sdk](https://dotnet.microsoft.com/en-us/download).
2. Copy all files inside dist folder of your Lab1 SPA project into [wwwroot](./wwwroot) folder. 
3. Run [CreateDockerImage.cmd](./Scripts/CreateDockerImage.cmd) . It builds this project and generates docker image tags `server:latest` and `server:1.0.0`  
4. Run [RunDockerImage.cmd](./Scripts/RunDockerImage.cmd). It runs the server. Your Lab1 SPA is available at `http://localhost` and the Swagger UI is available at `http://localhost/swagger`.

