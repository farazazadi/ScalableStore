using DemoStore.Services.CommandSide.Application.Products.DataTransferObjects;
using MediatR;

namespace DemoStore.Services.CommandSide.Application.Products.Commands.BuyProduct;
public sealed record BuyProductCommand(
    Guid ProductId,
    int Quantity

) : IRequest<BuyProductResultDto>;