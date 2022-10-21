FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY . .
WORKDIR /app/DemoStore.Services.QuerySide
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/DemoStore.Services.QuerySide/out ./

ENTRYPOINT ["dotnet", "DemoStore.Services.QuerySide.dll"]
