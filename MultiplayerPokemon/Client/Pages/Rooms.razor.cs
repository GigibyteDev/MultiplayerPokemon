using Fluxor;
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

        private bool canSpinFlag = true;

        private string refreshAnimationClass = string.Empty;

        private string newRoomName = string.Empty;

        private List<RoomData> roomData = new List<RoomData>();

        protected override async Task OnInitializedAsync()
        {
            await RefreshRoomData();

            await base.OnInitializedAsync();
        }

        private async Task handleRefreshAnimation()
        {
            if (canSpinFlag)
            {
                canSpinFlag = false;

                refreshAnimationClass = "spin-animation";

                StateHasChanged();

                await Task.Delay(1000);

                refreshAnimationClass = "";

                canSpinFlag = true;
            }
        }

        private async Task RefreshRoomData()
        {
            var result = await Http.GetFromJsonAsync<IEnumerable<RoomData>>("GetRoomListData");

            roomData = result?.ToList() ?? new List<RoomData>();
        }

        private async void HandleRefreshClick()
        {
            await RefreshRoomData();
            handleRefreshAnimation();
        }

        private async void HandleOnRoomConnect(string roomName)
        {
            if (ConnectionState.Value?.Connection is not null && !string.IsNullOrWhiteSpace(roomName))
            {
                await ConnectionState.Value.Connection.SendCoreAsync("ConnectToRoom", new object[] { roomName });
            }

            await RefreshRoomData();
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
