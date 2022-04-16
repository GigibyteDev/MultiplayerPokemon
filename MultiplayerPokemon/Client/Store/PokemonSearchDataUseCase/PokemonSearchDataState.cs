using Fluxor;

namespace MultiplayerPokemon.Client.Store.PokemonSearchDataUseCase
{
    [FeatureState]
    public class PokemonSearchDataState
    {
        public Dictionary<int, string> PokemonNames { get; set; }

        public PokemonSearchDataState()
        {
            PokemonNames = new Dictionary<int, string>();
        }

        public PokemonSearchDataState(PokemonSearchDataState previousState, Dictionary<int, string>? pokemonNames = null)
        {
            PokemonNames = pokemonNames ?? previousState.PokemonNames;
        }
    }
}
