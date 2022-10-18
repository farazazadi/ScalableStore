using DemoStore.Services.CommandSide.Application;
using DemoStore.Services.CommandSide.Infrastructure;
using DemoStore.Services.CommandSide.Infrastructure.Persistence.DbContextInitializer;
using DemoStore.Services.CommandSide.WebApi;
using DemoStore.Services.CommandSide.WebApi.Common.Middleware;
using DemoStore.Services.CommandSide.WebApi.Products;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWebApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContextInitializer = scope.ServiceProvider.GetRequiredService<IAppDbContextInitializer>();

    await dbContextInitializer.MigrateAsync();
    await dbContextInitializer.SeedAsync();
}


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "media")),
    RequestPath = "/media"
});

app
    .UseMiddleware<ExceptionHandlingMiddleware>()
    .UseHttpsRedirection();

app.MapProductEndpoints();

app.Run();