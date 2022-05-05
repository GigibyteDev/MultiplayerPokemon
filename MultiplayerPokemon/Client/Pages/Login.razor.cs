using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Shared.Dtos;
using System.Net.Http.Json;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class Login
    {
        [Inject]
        private HttpClient Http { get; set; }
        [Inject]
        private ILocalStorageService LocalStorage { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; }

        private UserLoginModel user = new UserLoginModel();

        private string errorMessage = string.Empty;
        private async Task HandleLogin()
        {
            LoginRequest request = new LoginRequest
            {
                Username = user.Username,
                Password = user.Password,
            };

            var result = await Http.PostAsJsonAsync("Login", request);

            LoginResult content = await result.Content.ReadFromJsonAsync<LoginResult>() ?? new LoginResult { ErrorMessage = "Payload Couldn't Deserialize" };

            if (content.Success)
            {
                await LocalStorage.SetItemAsync("jwt", content.JWT);
                await AuthStateProvider.GetAuthenticationStateAsync();
            }
            else
            {
                errorMessage = content.ErrorMessage;
            }
        }
    }
}
