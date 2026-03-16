using AutoMapper;
using MiniShop.Application.Interfaces;
using MiniShop.Application.Product;

namespace MiniShop.Api.GraphQL
{
    public class Mutation
    {
        public async Task<Guid> UpdateProduct(ProductsResponse product,
                [Service]IProductService service)
        {
            var pro = new ProductDto(product.ProductId, product.Name, product.Price, product.Quantity);
            var id= await  service.UpdateProductAsync(pro);
            return id;

        }

    }
}
