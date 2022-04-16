using Fluxor;
using Microsoft.AspNetCore.Components;
using MultiplayerPokemon.Client.Clients;
using MultiplayerPokemon.Client.Store.PokemonSearchDataUseCase.PokemonSearchDataActions;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class Index
    {
        [Inject]
        private GQLPokemonClient GQLPokemonClient { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (GQLPokemonClient is not null)
            {
                var pokemonNames = await GQLPokemonClient.GetPokemonNames();

                if (pokemonNames is not null)
                {
                    Dispatcher.Dispatch(new PopulatePokemonSearchDataAction(pokemonNames));
                }
            }

            await base.OnInitializedAsync();
        }
    }
}
