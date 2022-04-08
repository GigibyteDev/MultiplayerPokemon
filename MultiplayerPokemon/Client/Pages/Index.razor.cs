using Fluxor;
using Microsoft.AspNetCore.Components;
using MultiplayerPokemon.Client.Store.RoomUseCase;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class Index
    {
        [Inject]
        private IState<RoomState> RoomState { get; set; }
    }
}
