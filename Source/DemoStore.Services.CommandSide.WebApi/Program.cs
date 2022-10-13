using DemoStore.Services.CommandSide.Application;
using DemoStore.Services.CommandSide.Infrastructure;
using DemoStore.Services.CommandSide.Infrastructure.Persistence.DbContextInitializer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContextInitializer = scope.ServiceProvider.GetRequiredService<IAppDbContextInitializer>();

        await dbContextInitializer.MigrateAsync();
        await dbContextInitializer.SeedAsync();
    }
}

app.UseHttpsRedirection();

app.Run();