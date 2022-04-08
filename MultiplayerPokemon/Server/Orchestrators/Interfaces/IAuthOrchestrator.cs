using MultiplayerPokemon.Shared.Dtos;

namespace MultiplayerPokemon.Server.Orchestrators.Interfaces
{
    public interface IAuthOrchestrator
    {
        Task<RegisterResult> RegisterUser(RegisterRequest request);

        Task<LoginResult> Login(LoginRequest request);

        TokenValidationResult IsAuthorized(TokenValidationRequest request);
    }
}
