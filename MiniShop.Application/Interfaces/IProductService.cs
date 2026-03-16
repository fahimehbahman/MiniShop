using MiniShop.Application.Product;
using MiniShop.Domain.Entities;


namespace MiniShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(string name, decimal price, int stock);
        Task<Guid> UpdateProductAsync(ProductDto productDto);
        Task<ProductsResponse> GetProductByIdAsync(string id);
        Task<List<ProductsResponse>> GetAllAsync();
        Task<List<ProductsResponse>> GetProducts(string? search);

    }
}
