using Fluxor;
using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.RoomUseCase
{
    [FeatureState]
    public class RoomState
    {
        public string RoomName { get; set; }
        public string CreatorUsername { get; set; }
        public List<string> ModUsernames { get; set; }
        public List<UserModel> CurrentUsers { get; set; }
        public List<UserModel> UserHistory { get; set; }
        public ChatModel Chat { get; set; }
        public RoomPartyModel PokemonParty { get; set; }
        public PokemonModel? SearchedPokemon { get; set; }
        public bool IsLoadingSearchedPokemon { get; set; }
        public bool ErrorGettingSearchedPokemon { get; set; }
        public string SearchedPokemonGender { get; set; }
        public bool SearchedPokemonShiny { get; set; }
        public List<int> SelectedCards { get; set; }

        public RoomState()
        {
            RoomName = string.Empty;
            CreatorUsername = string.Empty;
            ModUsernames = new List<string>();
            CurrentUsers = new List<UserModel>();
            UserHistory = new List<UserModel>();
            Chat = new ChatModel();
            PokemonParty = new RoomPartyModel(new Dictionary<int, PartyCardModel>());
            SearchedPokemon = null;
            IsLoadingSearchedPokemon = false;
            ErrorGettingSearchedPokemon = false;
            SearchedPokemonGender = "male";
            SearchedPokemonShiny = false;
            SelectedCards = new List<int>();
        }

        public RoomState(RoomModel model, RoomPartyModel roomPartyModel)
        {
            RoomName = model.RoomName;
            CreatorUsername = model.CreatorUsername;
            ModUsernames = model.ModUsernames;
            CurrentUsers = model.CurrentUsers;
            UserHistory = model.UserHistory;
            Chat = model.Chat;
            PokemonParty = roomPartyModel;
            SearchedPokemon = null;
            IsLoadingSearchedPokemon = false;
            ErrorGettingSearchedPokemon = false;
            SearchedPokemonGender = "male";
            SearchedPokemonShiny = false;
            SelectedCards = new List<int>();
        }

        public RoomState(
            RoomState previousState, 
            string? roomName = null, 
            string? creatorUserName = null, 
            List<string>? modUsernames = null, 
            List<UserModel>? currentUsers = null, 
            List<UserModel>? userHistory = null, 
            ChatModel? chat = null, 
            RoomPartyModel? pokemonParty = null,
            PokemonModel? searchedPokemon = null,
            bool? isLoadingSearchedPokemon = null,
            bool? errorGettingSearchedPokemon = null,
            string? searchedPokemonGender = null,
            bool? searchedPokemonShiny = null,
            List<int>? selectedCards = null
            )
        {
            RoomName = roomName ?? previousState.RoomName;
            CreatorUsername = creatorUserName ?? previousState.CreatorUsername;
            ModUsernames = modUsernames ?? previousState.ModUsernames;
            CurrentUsers = currentUsers ?? previousState.CurrentUsers;
            UserHistory= userHistory ?? previousState.UserHistory;
            Chat = chat ?? previousState.Chat;
            PokemonParty = pokemonParty ?? previousState.PokemonParty;
            SearchedPokemon = searchedPokemon ?? previousState.SearchedPokemon;
            IsLoadingSearchedPokemon = isLoadingSearchedPokemon ?? previousState.IsLoadingSearchedPokemon;
            ErrorGettingSearchedPokemon = errorGettingSearchedPokemon ?? previousState.ErrorGettingSearchedPokemon;
            SearchedPokemonShiny = searchedPokemonShiny ?? previousState.SearchedPokemonShiny;
            SearchedPokemonGender = searchedPokemonGender ?? previousState.SearchedPokemonGender;
            SelectedCards = selectedCards ?? previousState.SelectedCards;
        }
    }
}
