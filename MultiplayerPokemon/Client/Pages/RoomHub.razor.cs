using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MultiplayerPokemon.Client.Store.RoomUseCase;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.Effects;
using MultiplayerPokemon.Client.Store.UserUseCase;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class RoomHub
    {
        [Inject]
        private IState<RoomState> RoomState { get; set; }

        [Inject]
        private IState<UserState> UserState { get; set; }

        [Inject]
        private IState<ConnectionState> ConnectionState { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthState { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (AuthState is not null)
            {
                var context = await AuthState.GetAuthenticationStateAsync();

                if (Dispatcher is not null && context is not null)
                {
                    Dispatcher.Dispatch(new ConnectionConnectEffect(UserState.Value.User, StateHasChanged));
                }
            }

            await base.OnInitializedAsync();
        }
    }
}
