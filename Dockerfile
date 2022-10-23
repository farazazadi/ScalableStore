FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY . .
WORKDIR /app/DemoStore.Services.ApiGateway
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/DemoStore.Services.ApiGateway/out ./

ENTRYPOINT ["dotnet", "DemoStore.Services.ApiGateway.dll"]
