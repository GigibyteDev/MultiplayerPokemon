using Microsoft.IdentityModel.Tokens;
using MultiplayerPokemon.Server.Orchestrators.Interfaces;
using MultiplayerPokemon.Server.Repositories.Interfaces;
using MultiplayerPokemon.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace MultiplayerPokemon.Server.Orchestrators
{
    public class AuthOrchestrator : IAuthOrchestrator
    {
        private readonly IUserRepository userRepo;

        public AuthOrchestrator(IUserRepository _userRepo)
        {
            userRepo = _userRepo;
        }

        public Shared.Dtos.TokenValidationResult IsAuthorized(TokenValidationRequest request)
        {
            bool tokenValidated = userRepo.AuthorizeToken(request.Token, out SecurityToken? securityToken);

            var jwtSecurityToken = (JwtSecurityToken?)securityToken;

            if (jwtSecurityToken is not null && tokenValidated)
                return new Shared.Dtos.TokenValidationResult
                {
                    IsValidToken = true
                };

            return new Shared.Dtos.TokenValidationResult
            {
                IsValidToken = false
            };
        }

        public async Task<LoginResult> Login(LoginRequest request)
        {
            return await userRepo.Login(request);
        }

        public async Task<RegisterResult> RegisterUser(RegisterRequest request)
        {
            return await userRepo.Register(request);
        }
    }
}
