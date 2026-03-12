using MiniShop.Application.Product;
using MiniShop.Domain.Entities;


namespace MiniShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(string name, decimal price, int stock);
        Task<List<MiniShop.Domain.Entities.Product>> GetAllAsync();
        Task<List<ProductsResponse>> GetProducts(string? search);

    }
}
