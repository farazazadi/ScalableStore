using DemoStore.Clients.WebUi.DataTransferObjects;

namespace DemoStore.Clients.WebUi.Services;

internal sealed class ProductService
{
    private readonly HttpClient _client;
    public readonly string BaseUrl;

    public ProductService(HttpClient client)
    {
        _client = client;
        BaseUrl = client.BaseAddress.AbsoluteUri;
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<IReadOnlyList<ProductDto>>("/api/products");
    }

    public async Task<BuyProductResultDto> Buy(Guid id, int qty)
    {
        var payload = new BuyProductDto(id, qty);
        var response = await _client.PutAsJsonAsync($"/api/products/{id}/buy", payload);
        return await response.Content.ReadFromJsonAsync<BuyProductResultDto>();
    }
}
