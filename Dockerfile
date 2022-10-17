FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY . .
WORKDIR /app/Source/DemoStore.Services.CommandSide.WebApi
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Source/DemoStore.Services.CommandSide.WebApi/out ./

ENTRYPOINT ["dotnet", "DemoStore.Services.CommandSide.WebApi.dll"]
