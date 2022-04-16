using MultiplayerPokemon.Client.Models;

namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class GetPokemonSuccessAction
    {
        public PokemonModel PokemonModel { get; set; }
        public GetPokemonSuccessAction(PokemonModel pokemonModel)
        {
            PokemonModel = pokemonModel;
        }
    }
}
