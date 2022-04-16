namespace MultiplayerPokemon.Client.Store.RoomUseCase.Effects
{
    public class GetPokemonEffect
    {
        public string PokemonId { get; set; }

        public GetPokemonEffect(string pokemonId)
        {
            PokemonId = pokemonId;
        }
    }
}
