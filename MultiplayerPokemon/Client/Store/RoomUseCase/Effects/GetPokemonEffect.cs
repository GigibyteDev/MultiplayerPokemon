namespace MultiplayerPokemon.Client.Store.RoomUseCase.Effects
{
    public class GetPokemonEffect
    {
        public string PokemonId { get; set; }

        public Action<string> UpdatePokemonName { get; set; }
        public GetPokemonEffect(string pokemonId, Action<string> updatePokemonName)
        {
            PokemonId = pokemonId;
            UpdatePokemonName = updatePokemonName;
        }
    }
}
