namespace MultiplayerPokemon.Shared.Models
{
    public class RoomModel
    {
        public string RoomName { get; set; }
        public string CreatorUsername { get; set; }
        public List<string> ModUsernames { get; set; }
        public List<UserModel> CurrentUsers { get; set; }
        public List<UserModel> UserHistory { get; set; }
        public ChatModel Chat { get; set; }
        public PartyModel PokemonParty { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
