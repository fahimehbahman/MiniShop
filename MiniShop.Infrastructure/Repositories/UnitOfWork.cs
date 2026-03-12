using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Interfaces;
using MiniShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopDbContext _context;

        public UnitOfWork(ShopDbContext context)
        {
            _context = context;
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
