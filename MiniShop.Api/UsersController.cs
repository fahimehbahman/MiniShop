using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using MiniShop.Application.Interfaces;

namespace MiniShop.Api
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController :ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService,ILogger<UsersController> logger) 
        {
            _logger = logger;
            _userService = userService;
        }

    }
}
