using MultiplayerPokemon.Client.Helpers;
using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Client.Models.DataModels;
using System.Net.Http.Json;

namespace MultiplayerPokemon.Client.Clients
{
    public class RESTPokemonClient
    {
        private readonly HttpClient http;

        public RESTPokemonClient(HttpClient _http)
        {
            http = _http;
        }

        public async Task<PokemonModel?> GetPokemonById(string id)
        {
            try
            {
                var pokemonData = await http.GetFromJsonAsync<PokemonData>($"pokemon/{id}");

                var speciesData = await http.GetFromJsonAsync<PokemonSpeciesData>($"pokemon-species/{pokemonData?.Species?.Url?.Trim('/').Split('/').Last()}");

                var formData = await http.GetFromJsonAsync<PokemonFormData>($"pokemon-form/{pokemonData?.Forms?[0].Url?.Trim('/').Split('/').Last()}");

                if (pokemonData is not null && speciesData is not null && formData is not null)
                    return pokemonData.MapRawPokemonDataToPokemonModel(speciesData, formData);
            }
            catch (Exception ex)
            {}
            return null;
        }

        public async Task<PokemonAltInformation?> GetPokemonAlt(string id)
        {
            try
            {
                var pokemonAlt = await http.GetFromJsonAsync<PokemonFormData>($"pokemon-form/{id}");
                return pokemonAlt?.MapPokemonFormDataToAltInformation();
            }
            catch (Exception ex)
            {}

            return null;
        }
    }
}
