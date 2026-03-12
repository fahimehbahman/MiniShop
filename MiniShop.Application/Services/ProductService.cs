using MiniShop.Application.Interfaces;
using MiniShop.Application.Product;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uw;
        public ProductService(IProductRepository productRepository, IUnitOfWork uw) 
        {
            _productRepository = productRepository;
            _uw = uw;
        }
        public async Task<Guid> CreateProductAsync(string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            if (price < 0)
                throw new ArgumentOutOfRangeException("Price must be greater than 0");
            if (stock < 0)
                throw new ArgumentOutOfRangeException("Quantity Must be greater than zero");
            var product=new MiniShop.Domain.Entities.Product(name, price, stock );

            await _productRepository.AddAsync(product);
            await _uw.CommitAsync();

            return product.ProductId;
        }

        public async Task<List<MiniShop.Domain.Entities.Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<List<ProductsResponse>> GetProducts(string? search)
        {
            List<MiniShop.Domain.Entities.Product> products;

            if (!string.IsNullOrWhiteSpace(search))
                products = await _productRepository.FilterByName(search);
            else
                products = await _productRepository.GetAllAsync();

          return  products.Select(p => new ProductsResponse
            (
                 p.ProductId,
                 p.Name,
                 p.Price,
                 p.Stock
            )).ToList();
        }
    }
}
