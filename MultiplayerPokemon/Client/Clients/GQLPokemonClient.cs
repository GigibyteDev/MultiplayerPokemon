using MultiplayerPokemon.Client.Helpers;
using MultiplayerPokemon.Client.Models.DataModels;
using System.Text;
using System.Text.Json;

namespace MultiplayerPokemon.Client.Clients
{
    public class GQLPokemonClient
    {
        private readonly HttpClient http;

        public GQLPokemonClient(HttpClient _http)
        {
            http = _http;
        }

        public async Task<Dictionary<int, string>> GetPokemonNames()
        {
            Dictionary<int, string> pokemonNames = new Dictionary<int, string>();

            var queryObject = new
            {
                query = @"query { pokemon_v2_pokemon { id, name } }",
                variables = new { }
            };

            var pokemonNameQuery = new StringContent(
                JsonSerializer.Serialize(queryObject),
                Encoding.UTF8,
                "application/json"
            );

            try
            {

                var result = await http.PostAsync("graphql/v1beta", pokemonNameQuery);

                if (result.IsSuccessStatusCode)
                {
                    var data = await JsonSerializer.DeserializeAsync<PokemonNameDataCollectionWrapper>(await result.Content.ReadAsStreamAsync());
                    if (data is not null)
                    {
                        foreach(var name in data.Data.PokemonNames)
                        {
                            pokemonNames.Add(name.Id, name.Name.ToDisplayName());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return pokemonNames;
        }
    }
}
