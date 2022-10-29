namespace DemoStore.Clients.WebUi.DataTransferObjects;

internal sealed class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ThumbnailUrl { get; set; }
}