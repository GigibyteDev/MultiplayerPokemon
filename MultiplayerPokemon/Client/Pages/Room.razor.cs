using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MultiplayerPokemon.Client.Clients;
using MultiplayerPokemon.Client.Helpers;
using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Client.Store.PokemonSearchDataUseCase;
using MultiplayerPokemon.Client.Store.RoomUseCase;
using MultiplayerPokemon.Client.Store.RoomUseCase.Effects;
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
        private IState<PokemonSearchDataState> SearchDataState { get; set; }


        private List<string> PokemonNames = new List<string>();
        private string messageText = string.Empty;
        private string pokemonId = "Pikachu";
        private bool roomTabToggle = true;
        private Dictionary<int, string[]> partyHoverClasses;
        private int lastHover = -1;
        private void ToggleToSearch()
        {
            roomTabToggle = true;
        }

        private void ToggleToParty()
        {
            roomTabToggle = false;
            ResetPartyHoverClasses();
        }

        private void UpdatePokemonName(string pokemonName)
        {
            pokemonId = pokemonName;
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

        private void HandleGetPokemon(string? overridePokemonName = null)
        {
            Dispatcher.Dispatch(new GetPokemonEffect(overridePokemonName?.FromDisplayName() ?? pokemonId.FromDisplayName()));

            pokemonId = overridePokemonName?.ToDisplayName() ?? pokemonId.ToDisplayName();

            StateHasChanged();
        }

        private async void HandleAddPokemonToParty()
        {
            StateHasChanged();
            if (RoomState.Value?.SearchedPokemon is not null)
            {
                if (ConnectionState.Value?.Connection is not null)
                {
                    await ConnectionState.Value.Connection.SendCoreAsync("AddPokemonToParty", new object[] 
                    { 
                        new PokemonPartyDataModel 
                        { 
                            PokedexId = RoomState.Value.SearchedPokemon.Id, 
                            Gender = RoomState.Value.SearchedPokemonGender, 
                            IsShiny = RoomState.Value.SearchedPokemonShiny
                        },
                        RoomState.Value.RoomName
                    });
                }

                Dispatcher.Dispatch(new AddPokemonToPartyAction(new PartyCardModel
                {
                    Id = RoomState.Value.SearchedPokemon.Id,
                    Name = RoomState.Value.SearchedPokemon.Name,
                    Gender = RoomState.Value.SearchedPokemonGender,
                    IsShiny = RoomState.Value.SearchedPokemonShiny,
                    ImageURI = RoomState.Value.SearchedPokemon.Sprites.OfficialArtwork,
                    Stats = RoomState.Value.SearchedPokemon.Stats,
                    Types = RoomState.Value.SearchedPokemon.Types
                }));
            }
        }

        private async Task<IEnumerable<string>> SearchPokemon(string searchText)
        {
            if (int.TryParse(searchText, out int pokedexId))
            {
                if (SearchDataState.Value.PokemonNames.TryGetValue(pokedexId, out string? pokemonName))
                {
                    return await Task.FromResult(new List<string>() { pokemonName });
                }
                else
                {
                    return await Task.FromResult(new List<string>());
                }
            }
            else
            {
                return await Task.FromResult(SearchDataState.Value.PokemonNames.Values.Where(p => p.ToLower().Contains(searchText.ToLower())).ToList());
            }
        }

        private void HandleHoverOverClasses(int partyId)
        {
            if (lastHover != -1)
            {
                partyHoverClasses[lastHover][0] = "options-background-shrink";
                partyHoverClasses[lastHover][1] = "option-shrink";
            }

            if (lastHover != partyId)
            {
                partyHoverClasses[partyId][0] = "options-background-grow";
                partyHoverClasses[partyId][1] = "option-grow";
            }

            lastHover = partyId;
        }

        private void ResetPartyHoverClasses()
        {
            partyHoverClasses = new Dictionary<int, string[]>();
            for (int i = 0; i < 6; i++)
            {
                partyHoverClasses.Add(i, new string[2] { string.Empty, string.Empty });
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ResetPartyHoverClasses();
        }
    }
}
