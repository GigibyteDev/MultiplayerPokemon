using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Helpers
{
    public static class TypeRelationshipHelper
    {
        public static TypeRelationshipCalculationResults CalculateTypeRelationsParty(IEnumerable<IEnumerable<PokemonTypes>> pokemonTypes)
        {
            var calcAllResults = new List<TypeRelationshipCalculationResults>();
            var calcFinalResult = new TypeRelationshipCalculationResults();
            foreach (var pokemonType in pokemonTypes)
            {
                calcAllResults.Add(CalculateTypeRelationsPokemon(pokemonType));
            }

            foreach(var pokemonType in PokemonTypes.PokemonTypesCollection)
            {
                if (calcAllResults.All(x => x.CalculatedPokemonRelationalTypes.Single(y => y.Key.Id == pokemonType.Id).Value == PokemonTypeCalculationResults.DefensiveWeakness))
                {
                    calcFinalResult.CalculatedPokemonRelationalTypes.Add(pokemonType, PokemonTypeCalculationResults.DefensiveWeakness);
                }
                else
                {
                    calcFinalResult.CalculatedPokemonRelationalTypes.Add(pokemonType, PokemonTypeCalculationResults.Default);
                }
            }

            return calcFinalResult;
        }

        public static TypeRelationshipCalculationResults CalculateTypeRelationsPokemon(IEnumerable<PokemonTypes> pokemonTypes)
        {
            TypeRelationshipCalculationResults calcResults = new TypeRelationshipCalculationResults();
            foreach (var pokemonType in PokemonTypes.PokemonTypesCollection)
            {
                int weaknessCount = 0;
                foreach(var type in pokemonTypes)
                {
                    var defenseVulnerabilities = TypeRelationships.TypeDefenseVulnerabilities.Single(x => x.Key.Id == type.Id).Value;

                    if (defenseVulnerabilities.Any(x => x.Id == pokemonType.Id))
                    {
                        weaknessCount++;
                    }
                }

                switch (weaknessCount)
                {
                    case 1:
                        calcResults.CalculatedPokemonRelationalTypes.Add(pokemonType, PokemonTypeCalculationResults.DefensiveWeakness);
                        break;
                    case 2:
                        calcResults.CalculatedPokemonRelationalTypes.Add(pokemonType, PokemonTypeCalculationResults.DefensiveWeakness);
                        break;
                }
            }

            return calcResults;
        }
    }

    public class TypeRelationshipCalculationResults
    {
        public Dictionary<PokemonTypes, PokemonTypeCalculationResults> CalculatedPokemonRelationalTypes { get; set; }

        public TypeRelationshipCalculationResults()
        {
            CalculatedPokemonRelationalTypes = new Dictionary<PokemonTypes, PokemonTypeCalculationResults>();
        }
    }

    public enum PokemonTypeCalculationResults
    {
        Default,
        OffensiveWeakness,
        DefensiveWeakness,
        DefensiveWeaknessDouble,
        Unhittable
    }
}
