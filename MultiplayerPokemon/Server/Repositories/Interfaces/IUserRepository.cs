using Microsoft.IdentityModel.Tokens;
using MultiplayerPokemon.Server.Models;
using MultiplayerPokemon.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace MultiplayerPokemon.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<RegisterResult> Register(RegisterRequest request);
        Task<LoginResult> Login(LoginRequest request);
        Task<User?> GetUserByUsername(string username);
        bool AuthorizeToken(string token, out SecurityToken? securityToken);
    }
}
