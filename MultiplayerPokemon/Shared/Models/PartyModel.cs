namespace MultiplayerPokemon.Shared.Models
{
    public class PartyModel
    {
        public Dictionary<int, PokemonPartyDataModel> Pokemon { get; set; }

        public PartyModel()
        {
            Pokemon = new Dictionary<int, PokemonPartyDataModel>();
        }
    }
}
