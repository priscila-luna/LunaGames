using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LunaGames.Models;
using LunaGames.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace LunaGames.Services;

public class AuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<string> Login(Login login)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
        User user = await _userRepository.Get(login.Username);
        
        if (user.Email == login.Username && user.Password == ComputeSha256Hash(login.Password))
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = new ClaimsIdentity(claims),
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        return "";
    }

    private string ComputeSha256Hash(string rawData)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();

            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}