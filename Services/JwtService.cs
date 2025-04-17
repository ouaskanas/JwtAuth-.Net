using jwt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> userManager;

        public JwtService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<string> GenerateToken(User user)
        {
            var Claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // audience should be imported from request header
            var token = new JwtSecurityToken(
                issuer : _configuration["JwtConfig:Issuer"],
                audience : _configuration["JwtConfig:Audience"],
                claims : Claims,
                expires : DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtConfig:ExpiryInMinutes"])),
                signingCredentials : creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
     }
}
