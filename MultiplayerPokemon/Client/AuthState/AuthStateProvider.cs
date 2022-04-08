using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MultiplayerPokemon.Shared.Dtos;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using Fluxor;
using MultiplayerPokemon.Client.Store.UserUseCase.UserActions;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.AuthState
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly HttpClient http;
        private readonly IDispatcher Dispatcher;

        public AuthStateProvider(ILocalStorageService _localStorage, HttpClient _http, IDispatcher dispatcher)
        {
            localStorage = _localStorage;
            http = _http;
            Dispatcher = dispatcher;
         }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());
            var jwt = await localStorage.GetItemAsStringAsync("jwt");
            if (!string.IsNullOrWhiteSpace(jwt))
            {
                jwt = jwt.Trim('\"');
                if (await ValidateCurrentToken(jwt))
                {
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                    var securityToken = new JwtSecurityTokenHandler().ReadToken(jwt) as JwtSecurityToken;
                    var claims = securityToken?.Claims ?? new Claim[0];
                    var identity = new ClaimsIdentity(claims, "jwt");

                    state = new AuthenticationState(new ClaimsPrincipal(identity));
                    var userModel = new UserModel
                    {
                        Id = Convert.ToInt32(claims.Single(c => c.Type == "nameid").Value),
                        Username = claims.Single(c => c.Type == "unique_name").Value,
                        IsAdmin = claims.FirstOrDefault(c => c.Type == "role")?.Value == "Administrator"
                    };

                    Dispatcher.Dispatch(new AddUserAction(userModel));
                }
                else
                {
                    await localStorage.RemoveItemAsync("jwt");
                }
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        private async Task<bool> ValidateCurrentToken(string token)
        {
            var isAuthorized = await http.PostAsJsonAsync("ValidateToken", new TokenValidationRequest { Token = token });

            var result = await isAuthorized.Content.ReadFromJsonAsync<TokenValidationResult>();

            return result?.IsValidToken ?? false;
        }
    }
}
