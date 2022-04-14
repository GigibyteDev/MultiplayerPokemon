using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Shared.Models
{
    public class PokemonPartyDataModel
    {
        public int PokedexId { get; set; }
        public int PositionInParty { get; set; }
        public string Gender { get; set; }
        public bool IsShiny { get; set; }
    }
}
