

namespace MiniShop.Api.Product
{
    public record ProductsRequest(
        Guid ProductId,
        string Name,
        decimal Price,
        int Quantity
    );
}
