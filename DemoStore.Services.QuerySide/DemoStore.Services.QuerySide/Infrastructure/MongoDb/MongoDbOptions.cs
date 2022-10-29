namespace DemoStore.Services.QuerySide.Infrastructure.MongoDb;

internal sealed class MongoDbOptions
{
    public static string SectionName => "MongoDb";
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
