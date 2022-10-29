namespace DemoStore.Clients.WebUi.DataTransferObjects;

public sealed record BuyProductResultDto(
    Guid Id,
    int RemainedQuantity
);