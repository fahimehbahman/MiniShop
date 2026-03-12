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
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDbContext _context;
        public OrderRepository(ShopDbContext context)
        {
           _context = context;
        }
        public async Task AddAsync(Order order)
        {
          await _context.Order.AddAsync(order);
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
           return await _context.Order.Where(x => x.OrderId == orderId)
                                .Include(x=>x.Items)
                                .ThenInclude(x=>x.Product)
                                .FirstOrDefaultAsync();
        }
    }
}
