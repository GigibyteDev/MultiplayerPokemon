using Fluxor;
using Microsoft.AspNetCore.Components;
using MultiplayerPokemon.Client.Store.RoomUseCase;

namespace MultiplayerPokemon.Client.Components
{
    public partial class SearchPokemonButton
    {
        [Parameter]
        public Action<string?> HandleGetPokemon { get; set; }

        [Inject]
        private IState<RoomState> RoomState { get; set; }
    }
}
