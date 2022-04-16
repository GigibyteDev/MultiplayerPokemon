using Fluxor;
using MultiplayerPokemon.Client.Store.PokemonSearchDataUseCase.PokemonSearchDataActions;

namespace MultiplayerPokemon.Client.Store.PokemonSearchDataUseCase
{
    public class PokemonSearchDataReducer
    {
        [ReducerMethod]
        public PokemonSearchDataState PopulatePokemonSearchDataAction(PokemonSearchDataState state, PopulatePokemonSearchDataAction action)
        {
            return new PokemonSearchDataState
            (
                previousState: state,
                pokemonNames: action.PokemonSearchData
            );
        }
    }
}
