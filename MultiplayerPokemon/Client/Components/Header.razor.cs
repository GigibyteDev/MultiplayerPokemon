using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MultiplayerPokemon.Client.Store.RoomUseCase;
using MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.ConnectionActions;
using MultiplayerPokemon.Client.Store.UserUseCase;
using MultiplayerPokemon.Client.Store.UserUseCase.UserActions;

namespace MultiplayerPokemon.Client.Components
{
    public partial class Header
    {
        [Inject]
        private IState<UserState> UserState { get; set; }

        [Inject]
        private IState<RoomState> RoomState { get; set; }

        [Inject]
        private IState<ConnectionState> ConnectionState { get; set; }

        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthState { get; set; }

        [Inject]
        private IDispatcher Dispatch { get; set; }

        bool expanded = false;
        string expandClass = string.Empty;
        string rotateClass = string.Empty;

        private async void HandleLogOut()
        {
            Dispatch.Dispatch(new RemoveUserAction());
            Dispatch.Dispatch(new DisconnectUserFromRoomAction());
            Dispatch.Dispatch(new CloseConnectionAction());
            await LocalStorage.RemoveItemAsync("jwt");
            await AuthState.GetAuthenticationStateAsync();
        }

        private async void HandleLeaveRoom()
        {
            if (ConnectionState.Value?.Connection is not null && RoomState.Value is not null)
            {
                await ConnectionState.Value.Connection.SendCoreAsync("UserDisconnectedRoom", new object[] { RoomState.Value.RoomName });
            }
        }

        private void ToggleExpand()
        {
            if (expanded)
            {
                expandClass = "unexpand";
                rotateClass = "unrotate";
            }
            else
            {
                expandClass = "expand";
                rotateClass = "rotate";
            }

            expanded = !expanded;
        }
    }
}
