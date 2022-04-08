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

        public TokenValidationResult IsAuthorized(TokenValidationRequest request)
        {
            var securityToken = (JwtSecurityToken?)userRepo.AuthorizeToken(request.Token);

            if (securityToken is not null)
                return new TokenValidationResult
                {
                    IsValidToken = true
                };

            return new TokenValidationResult
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
