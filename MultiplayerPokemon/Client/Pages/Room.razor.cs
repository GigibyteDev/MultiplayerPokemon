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

        private string messageText = string.Empty;
        private string pokemonId = "Pikachu";
        private bool roomTabToggle = true;
        private int currentlyHoveringCardId = -1;

        private void ToggleToSearch()
        {
            roomTabToggle = true;
        }

        private void ToggleToParty()
        {
            roomTabToggle = false;
        }

        private void UpdatePokemonName(string pokemonName)
        {
            pokemonId = pokemonName.ToDisplayName();
            StateHasChanged();
        }

        private async void HandleSendMessage()
        {
            if (Dispatcher is not null && RoomState.Value is not null && ConnectionState.Value?.Connection is not null && !string.IsNullOrWhiteSpace(messageText))
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
            Dispatcher.Dispatch(new GetPokemonEffect(overridePokemonName?.FromDisplayName() ?? pokemonId.FromDisplayName(), UpdatePokemonName));

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
                        RoomState.Value.SearchedPokemon.Name.ToDisplayName(),
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

        private void HandleOnCardClick(int cardID)
        {
            if (RoomState.Value.SelectedCards.Contains(cardID))
            {
                Dispatcher.Dispatch(new DeselectCardAction(cardID));
            }
            else
            {
                Dispatcher.Dispatch(new SelectCardAction(cardID));
            }
        }

        private void HandleGetPokemonAndSwitchTab()
        {
            if (RoomState.Value is not null && RoomState.Value.PokemonParty.Cards.Any() && RoomState.Value.SelectedCards.Any())
            {
                HandleGetPokemon(RoomState.Value.PokemonParty.Cards.First(p => p.Key == RoomState.Value.SelectedCards.First()).Value.Name);
                ToggleToSearch();
            }
        }

        private async void HandleRemoveSelectedPokemonFromParty()
        {
            if (ConnectionState.Value?.Connection is not null && RoomState.Value is not null && RoomState.Value.SelectedCards.Any())
            {
                await ConnectionState.Value.Connection.SendCoreAsync("RemoveMultiplePokemonFromParty", new object[] 
                { 
                    RoomState.Value.SelectedCards, 
                    RoomState.Value.PokemonParty.Cards.Where(c => RoomState.Value.SelectedCards.Contains(c.Key)).Select(n => n.Value.Name.ToDisplayName()),
                    RoomState.Value.RoomName 
                });
                Dispatcher.Dispatch(new RemoveMultiplePokemonFromPartyAction(RoomState.Value.SelectedCards));
            }
        }

        private async void HandleDrop(int cardID)
        {
            if (ConnectionState.Value?.Connection is not null && RoomState.Value is not null)
                await ConnectionState.Value.Connection.SendCoreAsync("SwapPokemon", new object[] { currentlyHoveringCardId, cardID, RoomState.Value.RoomName });
            Dispatcher.Dispatch(new PokemonSwappedAction(currentlyHoveringCardId, cardID));
        }

        private async Task HandleRemoveCardFromParty(int cardID)
        {
            if (ConnectionState.Value?.Connection is not null && RoomState.Value is not null)
                await ConnectionState.Value.Connection.SendCoreAsync("RemovePokemonFromParty", new object[] { cardID, RoomState.Value.RoomName });
            Dispatcher.Dispatch(new RemovePokemonFromPartyAction(cardID));
        }

        private void HandleDragStart(int cardID)
        {
            currentlyHoveringCardId = cardID;
        }

        private void HandleDragEnter(int cardID)
        {
        }

        private void HandleDragLeave(int cardID)
        {
        }

        protected override void OnInitialized()
        {
            if (RoomState.Value is not null && RoomState.Value.PokemonParty.Cards.Any())
            {
                var result = TypeRelationshipCalculator.CalculateTypeRelationsPokemon(RoomState.Value.PokemonParty.Cards.First().Value.Types);
            }

            base.OnInitialized();
        }
    }
}
