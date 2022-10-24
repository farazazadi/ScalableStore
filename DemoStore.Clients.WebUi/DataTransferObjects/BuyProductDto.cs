namespace DemoStore.Clients.WebUi.DataTransferObjects;

public sealed record BuyProductDto(
    Guid ProductId,
    int Quantity

);