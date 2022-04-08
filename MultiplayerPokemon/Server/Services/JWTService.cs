using Microsoft.IdentityModel.Tokens;
using MultiplayerPokemon.Server.Models;
using MultiplayerPokemon.Server.Services.Interfaces;
using MultiplayerPokemon.Server.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MultiplayerPokemon.Server.Services
{
    public class JWTService : ITokenService
    {
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }

            var mySecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AppSettings.JwtSecret));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                Issuer = AppSettings.Issuer,
                Audience = AppSettings.Audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha512Signature)
            };

            var jwt = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwt);
        }

        public SecurityToken? AuthorizeToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AppSettings.JwtSecret));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = AppSettings.Issuer,
                    ValidAudience = AppSettings.Audience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                return validatedToken;
            }
            catch
            {
                return null;
            }
        }
    }
}
