using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<MiniShop.Domain.Entities.Product?> GetByIdAsync(Guid productId);
        Task<List<MiniShop.Domain.Entities.Product>> GetAllAsync();
        Task<List<MiniShop.Domain.Entities.Product>> FilterByName(string search);
        Task AddAsync(MiniShop.Domain.Entities.Product product);
        Task<Guid> UpdateAsync(MiniShop.Domain.Entities.Product product);
    }
}
