using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatingApp.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Data
{
    public class AuthService
    {
        public static string GetUserToken(User user, int duration, string encryptionKey)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encryptionKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(duration),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            
        }
    }
}