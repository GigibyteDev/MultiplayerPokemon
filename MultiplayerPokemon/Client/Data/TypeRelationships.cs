using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Data
{
    public static class TypeRelationships
    {
        public static Dictionary<PokemonTypes, List<PokemonTypes>> TypeAttackStrengths { get; set; } = new Dictionary<PokemonTypes, List<PokemonTypes>>
        {
            { 
                PokemonTypes.Grass, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Ground, 
                    PokemonTypes.Rock, 
                    PokemonTypes.Water 
                } 
            },
            { 
                PokemonTypes.Fire,
                new List<PokemonTypes>
                { 
                    PokemonTypes.Bug, 
                    PokemonTypes.Grass, 
                    PokemonTypes.Ice, 
                    PokemonTypes.Steel 
                } 
            },
            { 
                PokemonTypes.Water,
                new List<PokemonTypes>
                { 
                    PokemonTypes.Fire, 
                    PokemonTypes.Ground,
                    PokemonTypes.Rock 
                } 
            },
            { 
                PokemonTypes.Normal,
                new List<PokemonTypes>() 
            },
            { 
                PokemonTypes.Bug, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Dark, 
                    PokemonTypes.Grass, 
                    PokemonTypes.Psychic 
                } 
            },
            { 
                PokemonTypes.Poison, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Fairy, 
                    PokemonTypes.Grass 
                } 
            },
            { 
                PokemonTypes.Flying, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Bug, 
                    PokemonTypes.Fighting, 
                    PokemonTypes.Grass 
                } 
            },
            { 
                PokemonTypes.Electric, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Flying, 
                    PokemonTypes.Water 
                } 
            },
            { 
                PokemonTypes.Ground,
                new List<PokemonTypes>
                { 
                    PokemonTypes.Electric, 
                    PokemonTypes.Fire, 
                    PokemonTypes.Poison, 
                    PokemonTypes.Rock, 
                    PokemonTypes.Steel 
                } 
            },
            { 
                PokemonTypes.Rock, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Bug, 
                    PokemonTypes.Fire, 
                    PokemonTypes.Flying, 
                    PokemonTypes.Ice 
                } 
            },
            { 
                PokemonTypes.Ice,
                new List<PokemonTypes>
                { 
                    PokemonTypes.Dragon, 
                    PokemonTypes.Flying, 
                    PokemonTypes.Ground, 
                    PokemonTypes.Grass 
                } 
            },
            { 
                PokemonTypes.Steel, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Fairy, 
                    PokemonTypes.Ice, 
                    PokemonTypes.Rock 
                } 
            },
            { 
                PokemonTypes.Fighting, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Dark, 
                    PokemonTypes.Ice, 
                    PokemonTypes.Normal, 
                    PokemonTypes.Rock, 
                    PokemonTypes.Steel 
                }
            },
            { 
                PokemonTypes.Dark, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Ghost, 
                    PokemonTypes.Psychic 
                } 
            },
            { 
                PokemonTypes.Ghost, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Ghost, 
                    PokemonTypes.Psychic 
                } 
            },
            { 
                PokemonTypes.Psychic, 
                new List<PokemonTypes>
                {
                    PokemonTypes.Fighting,
                    PokemonTypes.Poison 
                } 
            },
            { 
                PokemonTypes.Dragon,
                new List<PokemonTypes>
                { 
                    PokemonTypes.Dragon 
                } 
            },
            { 
                PokemonTypes.Fairy, 
                new List<PokemonTypes>
                { 
                    PokemonTypes.Dark, 
                    PokemonTypes.Dragon, 
                    PokemonTypes.Fighting 
                } 
            },
        };

        public static Dictionary<PokemonTypes, Dictionary<PokemonTypes, bool>> TypeAttackWeaknesses { get; set; } = new Dictionary<PokemonTypes, Dictionary<PokemonTypes, bool>>
        {
            {
                PokemonTypes.Grass, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Dragon, false },
                    { PokemonTypes.Flying, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Poison, false },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Fire, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Dragon, false },
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Rock, false },
                    { PokemonTypes.Water, false }
                }
            },
            {
                PokemonTypes.Water, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Dragon, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Water, false }
                }
            },
            {
                PokemonTypes.Normal, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Ghost, true },
                    { PokemonTypes.Rock, false },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Bug, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fairy, false },
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Flying, false },
                    { PokemonTypes.Fighting, false },
                    { PokemonTypes.Ghost, false },
                    { PokemonTypes.Poison, false },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Poison, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Ghost, false },
                    { PokemonTypes.Ground, false },
                    { PokemonTypes.Poison, false },
                    { PokemonTypes.Rock, false },
                    { PokemonTypes.Steel, true }
                }
            },
            {
                PokemonTypes.Flying, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Electric, false },
                    { PokemonTypes.Rock, false },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Electric, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Dragon, false },
                    { PokemonTypes.Electric, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Ground, true }
                }
            },
            {
                PokemonTypes.Ground, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Flying, true },
                    { PokemonTypes.Grass, false }
                }
            },
            {
                PokemonTypes.Rock, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fighting, false },
                    { PokemonTypes.Ground, false },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Ice, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Ice, false },
                    { PokemonTypes.Steel, false },
                    { PokemonTypes.Water, false }
                }
            },
            {
                PokemonTypes.Steel, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Electric, false },
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Steel, false },
                    { PokemonTypes.Water, false }
                }
            },
            {
                PokemonTypes.Fighting, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Fairy, false },
                    { PokemonTypes.Flying, false },
                    { PokemonTypes.Ghost, true },
                    { PokemonTypes.Poison, false },
                    { PokemonTypes.Psychic, false }
                }
            },
            {
                PokemonTypes.Dark, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Dark, false },
                    { PokemonTypes.Fairy, false },
                    { PokemonTypes.Fighting, false }
                }
            },
            {
                PokemonTypes.Ghost, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Dark, false },
                    { PokemonTypes.Normal, true }
                }
            },
            {
                PokemonTypes.Psychic, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Dark, true },
                    { PokemonTypes.Psychic, false },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Dragon, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fairy, true },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Fairy, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Poison, false },
                    { PokemonTypes.Steel, false }
                }
            }
        };

        public static Dictionary<PokemonTypes, Dictionary<PokemonTypes, bool>> TypeDefenseResistances { get; set; } = new Dictionary<PokemonTypes, Dictionary<PokemonTypes, bool>>
        {
            { 
                PokemonTypes.Grass, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Electric, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Ground, false },
                    { PokemonTypes.Water, false }
                }
            },
            { 
                PokemonTypes.Fire, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Ice, false },
                    { PokemonTypes.Steel, false }
                }
            },
            { 
                PokemonTypes.Water, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Ice, false },
                    { PokemonTypes.Steel, false },
                    { PokemonTypes.Water, false }
                }
            },
            { 
                PokemonTypes.Normal, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Ghost, true }
                }
            },
            { 
                PokemonTypes.Bug, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fighting, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Ground, false }
                }
            },
            { 
                PokemonTypes.Poison, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fairy, false },
                    { PokemonTypes.Fighting, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Poison, false }
                }
            },
            {
                PokemonTypes.Flying, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Fighting, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Ground, true }
                }
            },
            {
                PokemonTypes.Electric, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Electric, false },
                    { PokemonTypes.Flying, false },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Ground, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Electric, true },
                    { PokemonTypes.Poison, false },
                    { PokemonTypes.Rock, false }
                }
            },
            {
                PokemonTypes.Rock, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Flying, false },
                    { PokemonTypes.Normal, false },
                    { PokemonTypes.Poison, false }
                }
            },
            {
                PokemonTypes.Ice, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Ice, false }
                }
            },
            {
                PokemonTypes.Steel, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Dragon, false },
                    { PokemonTypes.Fairy, false },
                    { PokemonTypes.Flying, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Ice, false },
                    { PokemonTypes.Normal, false },
                    { PokemonTypes.Poison, true },
                    { PokemonTypes.Psychic, false },
                    { PokemonTypes.Rock, false },
                    { PokemonTypes.Steel, false }
                }
            },
            {
                PokemonTypes.Fighting, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Dark, false },
                    { PokemonTypes.Rock, false }
                }
            },
            {
                PokemonTypes.Dark, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Dark, false },
                    { PokemonTypes.Ghost, false },
                    { PokemonTypes.Psychic, true }
                }
            },
            {
                PokemonTypes.Ghost, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Fighting, true },
                    { PokemonTypes.Normal, true },
                    { PokemonTypes.Poison, false }
                }
            },
            {
                PokemonTypes.Psychic, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Fighting, false },
                    { PokemonTypes.Psychic, false }
                }
            },
            {
                PokemonTypes.Dragon, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Electric, false },
                    { PokemonTypes.Fire, false },
                    { PokemonTypes.Grass, false },
                    { PokemonTypes.Water, false }
                }
            },
            {
                PokemonTypes.Fairy, new Dictionary<PokemonTypes, bool>
                {
                    { PokemonTypes.Bug, false },
                    { PokemonTypes.Dark, false },
                    { PokemonTypes.Dragon, true },
                    { PokemonTypes.Fighting, false }
                }
            }
        };

        public static Dictionary<PokemonTypes, List<PokemonTypes>> TypeDefenseVulnerabilities { get; set; } = new Dictionary<PokemonTypes, List<PokemonTypes>>
        {
            {
                PokemonTypes.Grass,
                new List<PokemonTypes>
                {
                    PokemonTypes.Bug,
                    PokemonTypes.Fire,
                    PokemonTypes.Flying,
                    PokemonTypes.Ice,
                    PokemonTypes.Poison
                }
            },
            {
                PokemonTypes.Fire,
                new List<PokemonTypes>
                {
                    PokemonTypes.Ground,
                    PokemonTypes.Rock,
                    PokemonTypes.Water
                }
            },
            {
                PokemonTypes.Water,
                new List<PokemonTypes>
                {
                    PokemonTypes.Electric,
                    PokemonTypes.Grass
                }
            },
            {
                PokemonTypes.Normal,
                new List<PokemonTypes>
                {
                    PokemonTypes.Fighting
                }
            },
            {
                PokemonTypes.Bug,
                new List<PokemonTypes>
                {
                    PokemonTypes.Fire,
                    PokemonTypes.Flying,
                    PokemonTypes.Rock
                }
            },
            {
                PokemonTypes.Poison,
                new List<PokemonTypes>
                {
                    PokemonTypes.Ground,
                    PokemonTypes.Psychic
                }
            },
            {
                PokemonTypes.Flying,
                new List<PokemonTypes>
                {
                    PokemonTypes.Electric,
                    PokemonTypes.Ice,
                    PokemonTypes.Rock
                }
            },
            {
                PokemonTypes.Electric,
                new List<PokemonTypes>
                {
                    PokemonTypes.Ground
                }
            },
            {
                PokemonTypes.Ground,
                new List<PokemonTypes>
                {
                    PokemonTypes.Grass,
                    PokemonTypes.Ice,
                    PokemonTypes.Water
                }
            },
            {
                PokemonTypes.Rock,
                new List<PokemonTypes>
                {
                    PokemonTypes.Fighting,
                    PokemonTypes.Grass,
                    PokemonTypes.Ground,
                    PokemonTypes.Steel,
                    PokemonTypes.Water
                }
            },
            {
                PokemonTypes.Ice,
                new List<PokemonTypes>
                {
                    PokemonTypes.Fire,
                    PokemonTypes.Fighting,
                    PokemonTypes.Rock,
                    PokemonTypes.Steel
                }
            },
            {
                PokemonTypes.Steel,
                new List<PokemonTypes>
                {
                    PokemonTypes.Fighting,
                    PokemonTypes.Fire,
                    PokemonTypes.Ground
                }
            },
            {
                PokemonTypes.Fighting,
                new List<PokemonTypes>
                {
                    PokemonTypes.Fairy,
                    PokemonTypes.Flying,
                    PokemonTypes.Psychic
                }
            },
            {
                PokemonTypes.Dark,
                new List<PokemonTypes>
                {
                    PokemonTypes.Bug,
                    PokemonTypes.Fairy,
                    PokemonTypes.Fighting,
                }
            },
            {
                PokemonTypes.Ghost,
                new List<PokemonTypes>
                {
                    PokemonTypes.Dark,
                    PokemonTypes.Ghost,
                }
            },
            {
                PokemonTypes.Psychic,
                new List<PokemonTypes>
                {
                    PokemonTypes.Bug,
                    PokemonTypes.Dark,
                    PokemonTypes.Ghost
                }
            },
            {
                PokemonTypes.Dragon,
                new List<PokemonTypes>
                {
                    PokemonTypes.Dragon,
                    PokemonTypes.Ice,
                    PokemonTypes.Fairy
                }
            },
            {
                PokemonTypes.Fairy,
                new List<PokemonTypes>
                {
                    PokemonTypes.Poison,
                    PokemonTypes.Steel
                }
            },
        };
    }
}
