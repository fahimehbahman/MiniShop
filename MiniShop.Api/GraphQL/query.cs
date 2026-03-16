
using MiniShop.Application.Interfaces;
using MiniShop.Application.Product;


namespace MiniShop.Api.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<ProductsResponse>> GetProducts([Service] IProductService productService)
        {
            return await productService.GetAllAsync();
        }

        public async Task<ProductsResponse> GetProductbyId(string id,
        [Service] IProductService service)
        {
            return await service.GetProductByIdAsync(id);
        }


    }
}
