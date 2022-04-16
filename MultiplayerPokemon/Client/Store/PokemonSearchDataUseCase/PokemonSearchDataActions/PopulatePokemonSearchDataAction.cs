namespace MultiplayerPokemon.Client.Store.PokemonSearchDataUseCase.PokemonSearchDataActions
{
    public class PopulatePokemonSearchDataAction
    {
        public Dictionary<int, string> PokemonSearchData { get; set; }
        public PopulatePokemonSearchDataAction(Dictionary<int, string> pokemonSearchData)
        {
            PokemonSearchData = pokemonSearchData;
        }
    }
}
