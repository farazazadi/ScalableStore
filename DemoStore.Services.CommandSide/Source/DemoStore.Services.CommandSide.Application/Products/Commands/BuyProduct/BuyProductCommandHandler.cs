using DemoStore.Services.CommandSide.Application.Common.Contracts;
using DemoStore.Services.CommandSide.Application.Common.Exceptions;
using DemoStore.Services.CommandSide.Application.Products.DataTransferObjects;
using DemoStore.Services.CommandSide.Domain.Products;
using MediatR;

namespace DemoStore.Services.CommandSide.Application.Products.Commands.BuyProduct;

internal sealed class BuyProductCommandHandler : IRequestHandler<BuyProductCommand, BuyProductResultDto>
{
    private readonly IAppDbContext _dbContext;

    public BuyProductCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<BuyProductResultDto> Handle(BuyProductCommand command, CancellationToken token)
    {
        var product = await _dbContext.Products.FindAsync(new object[] {command.ProductId}, token);

        if (product is null)
            throw new EntityNotFoundException<Product>(command.ProductId);

        product.Buy(command.Quantity);

        await _dbContext.SaveChangesAsync(token);

        return new BuyProductResultDto(product.Id, product.Quantity);
    }
}