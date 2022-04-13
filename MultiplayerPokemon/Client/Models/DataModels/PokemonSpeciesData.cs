using System.Text.Json.Serialization;

namespace MultiplayerPokemon.Client.Models.DataModels
{
    public class PokemonSpeciesData
    {
        [JsonPropertyName("varieties")]
        public List<PokemonSpeciesDataVariety>? Varieties { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public List<PokemonSpeciesDataFlavorText>? FlavorTextEntries { get; set; }

        [JsonPropertyName("gender_rate")]
        public int GenderRate { get; set; } = -2;
    }

    public class PokemonSpeciesDataFlavorText
    {
        [JsonPropertyName("flavor_text")]
        public string? FlavorText { get; set; }

        [JsonPropertyName("language")]
        public PokemonSpeciesDataFlavorTextLanguage? Language { get; set; }

        [JsonPropertyName("version")]
        public PokemonSpeciesDataVersion? Version { get; set; }
    }

    public class PokemonSpeciesDataFlavorTextLanguage
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class PokemonSpeciesDataVersion
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class PokemonSpeciesDataVariety
    {
        [JsonPropertyName("pokemon")]
        public PokemonSpeciesDataVarietyPokemon? Pokemon { get; set; }
    }

    public class PokemonSpeciesDataVarietyPokemon
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
