using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Contracts.Login
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
    }
}
