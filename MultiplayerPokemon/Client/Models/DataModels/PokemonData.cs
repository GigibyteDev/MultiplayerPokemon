using System.Text.Json.Serialization;

namespace MultiplayerPokemon.Client.Models.DataModels
{
    public class PokemonData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("weight")]
        public float Weight { get; set; }

        [JsonPropertyName("height")]
        public float Height { get; set; }

        [JsonPropertyName("species")]
        public PokemonDataSpecies? Species { get; set; }

        [JsonPropertyName("stats")]
        public List<PokemonDataStat>? Stats { get; set; }

        [JsonPropertyName("forms")]
        public List<PokemonDataForm>? Forms { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonDataType>? Types { get; set; }

        [JsonPropertyName("sprites")]
        public PokemonDataSprites? Sprites { get; set; }
    }

    public class PokemonDataSprites
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }

        [JsonPropertyName("back_default")]
        public string? BackDefault { get; set; }

        [JsonPropertyName("front_shiny")]
        public string? FrontShiny { get; set; }

        [JsonPropertyName("back_shiny")]
        public string? BackShiny { get; set; }

        [JsonPropertyName("front_female")]
        public string? FrontFemale { get; set; }

        [JsonPropertyName("back_female")]
        public string? BackFemale { get; set; }

        [JsonPropertyName("front_shiny_female")]
        public string? FrontShinyFemale { get; set; }

        [JsonPropertyName("back_shiny_female")]
        public string? BackShinyFemale { get; set; }

        [JsonPropertyName("other")]
        public PokemonDataSpritesOtherSprites? Other { get; set; }
    }

    public class PokemonDataSpritesOtherSprites
    {
        [JsonPropertyName("official-artwork")]
        public PokemonDataSpritesOfficialArt? OfficialArtWork { get; set; }
    }

    public class PokemonDataSpritesOfficialArt
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }
    }

    public class PokemonAltData
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonDataType>? Types { get; set; }

        [JsonPropertyName("sprites")]
        public List<PokemonDataSprites>? Sprites { get; set; }
    }

    public class PokemonDataForm
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class PokemonDataSpecies
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class PokemonDataStat
    {
        [JsonPropertyName("base_stat")]
        public int StatBase { get; set; }

        [JsonPropertyName("stat")]
        public PokemonDataStatName? StatName { get; set; }
    }

    public class PokemonDataStatName
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class PokemonDataType
    {
        [JsonPropertyName("type")]
        public PokemonDataTypeData? Type { get; set; }
    }

    public class PokemonDataTypeData
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
