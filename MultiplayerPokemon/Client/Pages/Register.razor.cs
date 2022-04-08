using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Shared.Dtos;
using System.Net.Http.Json;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class Register
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; }


        private RegisterUserModel user = new RegisterUserModel();

        protected override async void OnInitialized()
        {
            if (!string.IsNullOrWhiteSpace(await LocalStorage.GetItemAsStringAsync("jwt")))
            {
                NavigationManager.NavigateTo("");
            }
            base.OnInitialized();
        }

        private async void HandleRegisterUser()
        {
            var registerRequest = new RegisterRequest
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };

            var registerResultData = await Http.PostAsJsonAsync("Register", registerRequest);

            registerResultData.EnsureSuccessStatusCode();

            var registerResult = await registerResultData.Content.ReadFromJsonAsync<RegisterResult>();

            if (registerResult?.Id != 0)
            {
                var loginResultData = await Http.PostAsJsonAsync("Login", new LoginRequest { Username = user.Username, Password = user.Password });

                loginResultData.EnsureSuccessStatusCode();

                var loginResult = await loginResultData.Content.ReadFromJsonAsync<LoginResult>();

                if (loginResult?.Success == true)
                {
                    await LocalStorage.SetItemAsync("jwt", loginResult.JWT);
                    await AuthStateProvider.GetAuthenticationStateAsync();
                    NavigationManager.NavigateTo("");
                }
            }
        }
    }
}
