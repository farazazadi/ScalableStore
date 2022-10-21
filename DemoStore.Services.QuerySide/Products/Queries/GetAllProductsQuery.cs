using DemoStore.Services.QuerySide.Common;
using MediatR;

namespace DemoStore.Services.QuerySide.Products.Queries;

internal record GetAllProductsQuery : IRequest<IList<ProductDto>>;


internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<ProductDto>>
{
    private readonly IRepository<Product> _repository;

    public GetAllProductsQueryHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<IList<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken token)
    {
        var products = await _repository.GetAllAsync(token);

        return products.ToProductDtoList();
    }
}