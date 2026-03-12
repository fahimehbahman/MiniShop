using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uw;

        public UserService(IUserRepository userRepository, IUnitOfWork uw)
        {
            _userRepository = userRepository;
            _uw = uw;
        }
        public async Task<Guid> CreateUserAsync(string userName, UserRole role)
        {
            var user = new User(userName, role);
            await _userRepository.AddAsync(user);
            await _uw.CommitAsync();
            return user.UserId;
        }

        public Task<User?> GetUserById(Guid id)
        {
           return _userRepository.GetByIdAsync(id);
        }
    }
}
