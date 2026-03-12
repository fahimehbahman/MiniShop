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
    public class UserRepository : IUserRepository
    {
        private readonly ShopDbContext _context;
        public UserRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
          => await _context.Users.AddAsync(user);
        public async Task<User?> GetByIdAsync(Guid userId)
          => await _context.Users.
                   Where(p => p.UserId == userId)
                   .FirstOrDefaultAsync();

        public async Task<User?> GetByUserNameAsync(string userName)
             => await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }
}
