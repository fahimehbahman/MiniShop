using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _context;
        public ProductRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
           => await _context.Products.AddAsync(product);
        

        public async Task<List<Product>> GetAllAsync()
           => await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid productId)
           =>  await _context.Products.
                           Where(p => p.ProductId == productId)
                           .FirstOrDefaultAsync();

        public async Task<List<Product>> FilterByName(string search)
            => await _context.Products.
                              Where(predicate => predicate.Name.Contains(search))
                             .ToListAsync();
        public async Task<Guid> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
        }
    }
}
