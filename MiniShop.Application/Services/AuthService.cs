using MiniShop.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwt;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwt)
        {
            _userRepository = userRepository;
            _jwt = jwt;
        }
        public async Task<string> LoginAsync(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName)
                        ?? throw new InvalidOperationException("User not found");

            return _jwt.Generate(user);
        }
    }
}
