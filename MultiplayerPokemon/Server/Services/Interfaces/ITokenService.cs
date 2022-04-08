using Microsoft.IdentityModel.Tokens;
using MultiplayerPokemon.Server.Models;
using System.IdentityModel.Tokens.Jwt;

namespace MultiplayerPokemon.Server.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
        SecurityToken? AuthorizeToken(string token);
    }
}
