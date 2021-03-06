using Microsoft.IdentityModel.Tokens;
using MultiplayerPokemon.Server.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace MultiplayerPokemon.Server.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepo)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token is not null)
                await AttachUserToContext(context, userRepo, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserRepository userRepo, string token)
        {
            bool tokenValidated = userRepo.AuthorizeToken(token, out SecurityToken? securityToken);

            var jwtToken = (JwtSecurityToken?)securityToken;

            if (jwtToken is not null && tokenValidated)
            {
                string username = jwtToken.Claims.First(x => x.Type == "unique_name").Value;
                context.Items["User"] = await userRepo.GetUserByUsername(username);
            }
        }
    }
}
