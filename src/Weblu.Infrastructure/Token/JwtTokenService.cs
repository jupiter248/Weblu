using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Weblu.Application.Exceptions;
using Weblu.Domain.Errors.Tokens;
using Weblu.Infrastructure.Identity;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Token
{
    public class JwtTokenService
    {
        public static string GenerateAccessToken(AppUser user, IList<string> roles)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_Key") ?? throw new NotFoundException(TokenErrorCodes.JwtKeyNoFound)));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = Environment.GetEnvironmentVariable("JWT_Issuer"),
                Audience = Environment.GetEnvironmentVariable("JWT_Audience"),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = creds,
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public static string GenerateRefreshToken()
        {
            Byte[] randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}