using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MiniShop.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Generate(User user)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"]!);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(secretKey!));

            var creds = new SigningCredentials(
                  key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                        expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                        claims: claims,
                        signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
