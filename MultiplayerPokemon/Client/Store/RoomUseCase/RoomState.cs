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

        public RoomState()
        {
            RoomName = string.Empty;
            CreatorUsername = string.Empty;
            ModUsernames = new List<string>();
            CurrentUsers = new List<UserModel>();
            UserHistory = new List<UserModel>();
            Chat = new ChatModel();
            PokemonParty = new RoomPartyModel(new List<PartyCardModel>());
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
        }

        public RoomState(
            RoomState previousState, 
            string? roomName = null, 
            string? creatorUserName = null, 
            List<string>? modUsernames = null, 
            List<UserModel>? currentUsers = null, 
            List<UserModel>? userHistory = null, 
            ChatModel? chat = null, 
            RoomPartyModel? pokemonParty = null
            )
        {
            RoomName = roomName ?? previousState.RoomName;
            CreatorUsername = creatorUserName ?? previousState.CreatorUsername;
            ModUsernames = modUsernames ?? previousState.ModUsernames;
            CurrentUsers = currentUsers ?? previousState.CurrentUsers;
            UserHistory= userHistory ?? previousState.UserHistory;
            Chat = chat ?? previousState.Chat;
            PokemonParty = pokemonParty ?? previousState.PokemonParty;
        }
    }
}
