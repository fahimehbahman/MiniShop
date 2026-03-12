

namespace MiniShop.Application.Product
{
    public record ProductsResponse(
        Guid ProductId,
        string Name,
        decimal Price,
        int Quantity
    );
}
