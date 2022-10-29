namespace DemoStore.Services.CommandSide.Infrastructure.Services.RabbitMq;
internal class RabbitMqOptions
{
    public static string SectionName => "RabbitMq";
    public string HostName { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }
    public bool AutomaticRecoveryEnabled { get; set; }
}
