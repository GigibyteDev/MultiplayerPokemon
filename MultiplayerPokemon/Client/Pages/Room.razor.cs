using Fluxor;
using Microsoft.AspNetCore.Components;
using MultiplayerPokemon.Client.Clients;
using MultiplayerPokemon.Client.Helpers;
using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Client.Store.RoomUseCase;
using MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase;
using MultiplayerPokemon.Client.Store.UserUseCase;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class Room
    {
        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        private IState<RoomState> RoomState { get; set; }

        [Inject]
        private IState<ConnectionState> ConnectionState { get; set; }

        [Inject]
        private IState<UserState> UserState { get; set; }

        [Inject]
        private GQLPokemonClient GQLPokemonClient { get; set; }

        [Inject]
        private RESTPokemonClient RESTPokemonClient { get; set; }


        private List<string> PokemonNames = new List<string>();
        private string messageText = string.Empty;
        private string pokemonId = "Pikachu";
        private PokemonModel? searchedPokemon;
        private bool roomTabToggle = true;

        protected override async Task OnInitializedAsync()
        {
            if (GQLPokemonClient is not null)
            {
                var pokemonNames = await GQLPokemonClient.GetPokemonNames();
                PokemonNames = pokemonNames.ToList();
            }

            await base.OnInitializedAsync();
        }

        private void ToggleToSearch()
        {
            roomTabToggle = true;
        }

        private void ToggleToParty()
        {
            roomTabToggle = false;
        }

        private async void HandleSendMessage()
        {
            if (Dispatcher is not null && RoomState.Value is not null && ConnectionState.Value?.Connection is not null)
            {
                await ConnectionState.Value.Connection.SendCoreAsync("SendMessageToRoom", new object[] { messageText, RoomState.Value.RoomName });
                Dispatcher.Dispatch(new AddMessageAction(new MessageModel
                {
                    MessageText = messageText,
                    User = UserState.Value.User,
                    SentDate = DateTime.Now
                }));

                messageText = string.Empty;
                StateHasChanged();
            }
        }

        private async void HandleGetPokemon(string? overridePokemonName = null)
        {
            if (RESTPokemonClient is not null)
            {
                searchedPokemon = await RESTPokemonClient.GetPokemonById(overridePokemonName?.FromDisplayName() ?? pokemonId.FromDisplayName().ToLower());

                pokemonId = searchedPokemon?.Name.ToDisplayName() ?? pokemonId;
                StateHasChanged();
            }
        }

        private async Task<IEnumerable<string>> SearchPokemon(string searchText)
        {
            return await Task.FromResult(PokemonNames.Where(p => p.ToLower().Contains(searchText.ToLower())).ToList());
        }
    }
}
