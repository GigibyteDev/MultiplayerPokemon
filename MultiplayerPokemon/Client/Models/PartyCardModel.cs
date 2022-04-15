using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Models
{
    public class PartyCardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public List<PokemonStat> Stats { get; set; }
        public List<PokemonTypes> Types { get; set; }
        public bool IsShiny { get; set; }
        public string ImageURI { get; set; }
    }
}
