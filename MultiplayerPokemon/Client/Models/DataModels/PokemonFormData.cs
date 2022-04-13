using System.Text.Json.Serialization;

namespace MultiplayerPokemon.Client.Models.DataModels
{
    public class PokemonFormData
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("is_battle_only")]
        public bool IsBattleOnly { get; set; }

        [JsonPropertyName("sprites")]
        public PokemonDataSprites Sprites { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonDataType>? Types { get; set; }
    }
}
