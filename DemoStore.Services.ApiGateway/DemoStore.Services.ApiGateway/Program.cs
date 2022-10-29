using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var ocelotConfigFile = builder.Environment.EnvironmentName == "Docker" ? "ocelot.Docker.json" : "ocelot.json";
builder.Configuration
    .AddJsonFile(ocelotConfigFile, false, true);


builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseOcelot();

app.Run();