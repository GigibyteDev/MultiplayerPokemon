using Microsoft.IdentityModel.Tokens;
using MultiplayerPokemon.Server.Models;
using MultiplayerPokemon.Server.Repositories.Interfaces;
using MultiplayerPokemon.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace MultiplayerPokemon.Server.Repositories
{
    // To be utilized to persist user login information in a database
    public class DBUserRepository : IUserRepository
    {
        public SecurityToken? AuthorizeToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResult> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterResult> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
