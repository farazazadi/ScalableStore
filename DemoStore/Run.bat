docker compose build
docker compose up -d
timeout /t 5 /nobreak

start http://localhost:8020
dotnet run -c release --project ..\DemoStore.Clients.WebUi\DemoStore.Clients.WebUi