using Microsoft.AspNetCore.Components;
using MultiplayerPokemon.Client.Helpers;
using MultiplayerPokemon.Client.Models;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class PartyStats
    {
        [Parameter]
        public IEnumerable<PartyCardModel> PartyPokemon { get; set; }

        private PartyTypeCoverageCalculationResults? partyTypes { get; set; } = null;

        private List<PokemonStat> totalStats = new List<PokemonStat>();

        protected override void OnParametersSet()
        {
            totalStats = new List<PokemonStat>();
            if (PartyPokemon is not null && PartyPokemon.Any())
            {
                partyTypes = TypeRelationshipCalculator.CalculateTypeRelationsParty(PartyPokemon.Select(x => x.Types));

                foreach(var stat in PartyPokemon.First().Stats)
                {
                    int statTotals = 0;
                    foreach (var partyPokemon in PartyPokemon)
                    {
                        statTotals += partyPokemon.Stats.First(s => s.Name == stat.Name).Base;
                    }
                    totalStats.Add(new PokemonStat(statTotals, stat.Name));
                }
            }

            base.OnParametersSet();
        }
    }
}
