using MiniShop.Application.Interfaces;
using Prometheus;
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
        private static readonly Counter UserLoggiedIn =
        Metrics.CreateCounter(
            "minishop_user_loggedin_total",
            "Total number of user loggin"
        );
        private static readonly Counter UserCantNotLoggedin =
            Metrics.CreateCounter(
                "minishop_user_cannot_loggedin_total",
                "Total number of user loggin"
            );

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwt)
        {
            _userRepository = userRepository;
            _jwt = jwt;
        }
        public async Task<string> LoginAsync(string userName)
        {
            try
            {
                var user = await _userRepository.GetByUserNameAsync(userName)
                  ?? throw new InvalidOperationException("User not found");
                UserLoggiedIn.Inc();
                return _jwt.Generate(user);
            }
            catch (Exception)
            {
                UserCantNotLoggedin.Inc();
                throw;
            }


        }
    }
}
