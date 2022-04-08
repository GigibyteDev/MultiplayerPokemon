﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.Effects;
using MultiplayerPokemon.Client.Store.UserUseCase;
using MultiplayerPokemon.Shared.Dtos;
using System.Net.Http.Json;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class Rooms
    {
        [Inject]
        private IDispatcher? Dispatcher { get; set; }

        [Inject]
        private AuthenticationStateProvider? AuthStateProvider { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private IState<UserState> UserState { get; set; }

        [Inject]
        private IState<ConnectionState> ConnectionState { get; set; }

        private string roomName = string.Empty;
        private string newRoomName = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (AuthStateProvider is not null)
            {
                var context = await AuthStateProvider.GetAuthenticationStateAsync();

                if (Dispatcher is not null && context is not null)
                {
                    Dispatcher.Dispatch(new ConnectionConnectEffect(UserState.Value.User, StateHasChanged));
                }
            }

            await base.OnInitializedAsync();
        }

        private void HandleOnRoomConnect()
        {
            if (ConnectionState.Value?.Connection is not null && !string.IsNullOrWhiteSpace(roomName))
            {
                ConnectionState.Value.Connection.SendCoreAsync("ConnectToRoom", new object[] { roomName });
            }
        }

        private async void HandleAddRoom()
        {
            if (UserState is not null && !string.IsNullOrWhiteSpace(newRoomName))
            {
                CreateRoomRequest request = new CreateRoomRequest
                {
                    RoomName = newRoomName,
                    Username = UserState.Value.User.Username
                };

                var response = await Http.PostAsJsonAsync("CreateRoom", request);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<CreateRoomResult>();

                if (result?.Success == true && ConnectionState.Value?.Connection is not null)
                {
                    await ConnectionState.Value.Connection.SendCoreAsync("ConnectToRoom", new object[] { newRoomName });
                }
            }
        }
    }
}
