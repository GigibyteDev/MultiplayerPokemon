using Microsoft.AspNetCore.Mvc;
using MultiplayerPokemon.Server.Orchestrators.Interfaces;
using MultiplayerPokemon.Shared.Dtos;

namespace MultiplayerPokemon.Server.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthOrchestrator authOrch;

        public AuthController(IAuthOrchestrator _authOrch)
        {
            authOrch = _authOrch;
        }

        [HttpPost("Register")]
        public async Task<RegisterResult> Register([FromBody] RegisterRequest request)
        {
            return await authOrch.RegisterUser(request);
        }

        [HttpPost("Login")]
        public async Task<LoginResult> Login([FromBody] LoginRequest request)
        {
            return await authOrch.Login(request);
        }

        [HttpPost("ValidateToken")]
        public TokenValidationResult ValidateToken([FromBody] TokenValidationRequest request)
        {
            return authOrch.IsAuthorized(request);
        }
    }
}
