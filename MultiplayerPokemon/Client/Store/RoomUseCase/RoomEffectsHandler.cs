using Fluxor;
using MultiplayerPokemon.Client.Clients;
using MultiplayerPokemon.Client.Store.RoomUseCase.Effects;
using MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions;

namespace MultiplayerPokemon.Client.Store.RoomUseCase
{
    public class RoomEffectsHandler
    {
        private readonly RESTPokemonClient pokemonClient;
        public RoomEffectsHandler(RESTPokemonClient pokemonClient)
        {
            this.pokemonClient = pokemonClient;
        }

        [EffectMethod]
        public async Task HandleGetPokemonEffect(GetPokemonEffect action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new GetPokemonStarterAction());

            var pokemonModel = await pokemonClient.GetPokemonById(action.PokemonId);

            if (pokemonModel is not null)
            {
                dispatcher.Dispatch(new GetPokemonSuccessAction(pokemonModel));
                action.UpdatePokemonName(pokemonModel.Name);
            }
            else
            {
                dispatcher.Dispatch(new GetPokemonErrorAction());
            }
        }
    }
}
