using Microsoft.AspNetCore.Components;
using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Components
{
    public partial class TypeIcons
    {
        [Parameter]
        public List<PokemonTypes> PokemonTypes { get; set; } = new List<PokemonTypes>();

        [Parameter]
        public int IconSize { get; set; } = 40;
    }
}
