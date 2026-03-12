using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Interfaces
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(string userName, UserRole Role);
       Task<User?> GetUserById(Guid id);
    }
}
