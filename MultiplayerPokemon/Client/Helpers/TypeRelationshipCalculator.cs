using MultiplayerPokemon.Client.Data;
using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Helpers
{
    public static class TypeRelationshipCalculator
    {
        public static PartyTypeCoverageCalculationResults CalculateTypeRelationsParty(IEnumerable<IEnumerable<PokemonTypes>> pokemonTypes)
        {
            var calcAllResults = new List<PokemonTypeRelationshipCalculationResults>();
            var calcFinalResult = new PartyTypeCoverageCalculationResults();
            
            foreach(var pokemonTypeSet in pokemonTypes)
            {
                calcAllResults.Add(CalculateTypeRelationsPokemon(pokemonTypeSet));
            }

            try
            {
                foreach(PokemonTypes type in PokemonTypes.PokemonTypesCollection)
                {
                    List<PartyCoverage> coveragesForType = new List<PartyCoverage>();
                    if (calcAllResults.All(x =>
                        x.CalculatedRelationalTypes.Single(y => y.Type.Id == type.Id).DefenseTier == TypeDefenseTiers.DefensiveWeakness ||
                        x.CalculatedRelationalTypes.Single(y => y.Type.Id == type.Id).DefenseTier == TypeDefenseTiers.DefensiveWeaknessDouble
                    ))
                    {
                        coveragesForType.Add(PartyCoverage.DefensivelyOpen);
                    }

                    if (calcAllResults.Any(x =>
                        x.CalculatedRelationalTypes.Single(y => y.Type.Id == type.Id).OffenseTier == TypeOffenseTiers.OffensiveStrength ||
                        x.CalculatedRelationalTypes.Single(y => y.Type.Id == type.Id).OffenseTier == TypeOffenseTiers.OffensiveStrengthDouble
                    ))
                    {
                        coveragesForType.Add(PartyCoverage.OffensivelyCovered);
                    }
                    
                    calcFinalResult.TypesCoverage.Add(type, coveragesForType);
                }
            }
            catch (Exception ex)
            {

            }

            return calcFinalResult;
        }

        public static PokemonTypeRelationshipCalculationResults CalculateTypeRelationsPokemon(IEnumerable<PokemonTypes> pokemonTypes)
        {
            PokemonTypeRelationshipCalculationResults calcResults = new PokemonTypeRelationshipCalculationResults();
            
            foreach(PokemonTypes type in PokemonTypes.PokemonTypesCollection)
            {
                int defensiveWeaknessCounter = 0;
                bool isDefenseUnaffected = false;
                foreach(PokemonTypes pokemonType in pokemonTypes)
                {
                    if (TypeRelationships.TypeDefenseVulnerabilities[pokemonType].Contains(type))
                        defensiveWeaknessCounter++;

                    if (TypeRelationships.TypeDefenseResistances.Single(x => x.Key.Id == pokemonType.Id).Value.TryGetValue(type, out bool unaffected))
                    {
                        if (unaffected)
                        {
                            isDefenseUnaffected = true;
                            break;
                        }
                        defensiveWeaknessCounter--;
                    }
                }

                int offensiveWeaknessCounter = 0;
                bool isOffenseUnaffected = false;
                foreach(PokemonTypes pokemonType in pokemonTypes)
                {
                    if (TypeRelationships.TypeAttackStrengths[pokemonType].Contains(type))
                        offensiveWeaknessCounter++;

                    if (TypeRelationships.TypeAttackWeaknesses[pokemonType].TryGetValue(type, out bool unaffected))
                    {
                        if (unaffected)
                        {
                            isOffenseUnaffected = true;
                            break;
                        }
                        offensiveWeaknessCounter--;
                    }
                }

                TypeDefenseTiers defenseTypeResult = TypeDefenseTiers.Default;

                if (isDefenseUnaffected)
                {
                    defenseTypeResult = TypeDefenseTiers.DefensiveNotEffected;
                }
                else
                {
                    switch (defensiveWeaknessCounter)
                    {
                        case 2:
                            defenseTypeResult = TypeDefenseTiers.DefensiveWeaknessDouble;
                            break;
                        case 1:
                            defenseTypeResult = TypeDefenseTiers.DefensiveWeakness;
                            break;
                        case -1:
                            defenseTypeResult = TypeDefenseTiers.DefensiveStrength;
                            break;
                        case -2:
                            defenseTypeResult = TypeDefenseTiers.DefensiveStrengthDouble;
                            break;
                    }
                }

                TypeOffenseTiers offenseTypeResult = TypeOffenseTiers.Default;

                if (isOffenseUnaffected)
                {
                    offenseTypeResult = TypeOffenseTiers.OffensiveNotEffected;
                }
                else
                {
                    switch (offensiveWeaknessCounter)
                    {
                        case 2:
                            offenseTypeResult = TypeOffenseTiers.OffensiveStrengthDouble;
                            break;
                        case 1:
                            offenseTypeResult = TypeOffenseTiers.OffensiveStrength;
                            break;
                        case -1:
                            offenseTypeResult = TypeOffenseTiers.OffensiveWeakness;
                            break;
                        case -2:
                            offenseTypeResult = TypeOffenseTiers.OffensiveWeaknessDouble;
                            break;
                    }
                }

                calcResults.CalculatedRelationalTypes.Add(new PokemonTypeRelationshipCalculationResults.TypeRelations(type, defenseTypeResult, offenseTypeResult));
            }

            return calcResults;
        }
    }

    public class PokemonTypeRelationshipCalculationResults
    {
        public List<TypeRelations> CalculatedRelationalTypes { get; set; }

        public PokemonTypeRelationshipCalculationResults()
        {
            CalculatedRelationalTypes = new List<TypeRelations>();   
        }

        public class TypeRelations
        {
            public PokemonTypes Type { get; set; }
            public TypeDefenseTiers DefenseTier { get; set; }
            public TypeOffenseTiers OffenseTier { get; set; }

            public TypeRelations(PokemonTypes type, TypeDefenseTiers defenseTier, TypeOffenseTiers offenseTiers)
            {
                Type = type;
                DefenseTier = defenseTier;
                OffenseTier = offenseTiers;
            }
        }
    }

    public class PartyTypeCoverageCalculationResults
    {
        public Dictionary<PokemonTypes, List<PartyCoverage>> TypesCoverage { get; set; }

        public PartyTypeCoverageCalculationResults()
        {
            TypesCoverage = new Dictionary<PokemonTypes, List<PartyCoverage>>();
        }
    }

    public enum TypeDefenseTiers
    {
        Default,
        DefensiveStrength,
        DefensiveStrengthDouble,
        DefensiveWeakness,
        DefensiveWeaknessDouble,
        DefensiveNotEffected
    }

    public enum TypeOffenseTiers
    {
        Default,
        OffensiveStrength,
        OffensiveStrengthDouble,
        OffensiveWeakness,
        OffensiveWeaknessDouble,
        OffensiveNotEffected
    }

    public enum PartyCoverage
    {
        NotCovered,
        OffensivelyCovered,
        DefensivelyOpen
    }
}
