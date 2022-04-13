using System.Text.Json.Serialization;

namespace MultiplayerPokemon.Client.Models.DataModels
{
    public class PokemonNameDataCollectionWrapper
    {
        [JsonPropertyName("data")]
        public PokemonNameDataCollection Data { get; set; }
    }

    public class PokemonNameDataCollection
    {
        [JsonPropertyName("pokemon_v2_pokemon")]
        public List<PokemonNameData> PokemonNames { get; set; }
    }

    public class PokemonNameData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
