namespace MultiplayerPokemon.Shared.Enums
{
    public class PokemonTypes
    {
        public string Id { get; }
        public string DisplayValue { get; }
        private string URI { get; set; }

        public static List<PokemonTypes> PokemonTypesCollection = new List<PokemonTypes>();

        public static readonly PokemonTypes Normal = new PokemonTypes("normal", "Normal", @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/aa/Pok%C3%A9mon_Normal_Type_Icon.svg/{}px-Pok%C3%A9mon_Normal_Type_Icon.svg.png");
        public static readonly PokemonTypes Fighting = new PokemonTypes("fighting", "Fighting", @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/Pok%C3%A9mon_Fighting_Type_Icon.svg/{}px-Pok%C3%A9mon_Fighting_Type_Icon.svg.png");
        public static readonly PokemonTypes Flying = new PokemonTypes("flying", "Flying", @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/Pok%C3%A9mon_Flying_Type_Icon.svg/{}px-Pok%C3%A9mon_Flying_Type_Icon.svg.png");
        public static readonly PokemonTypes Poison = new PokemonTypes("poison", "Poison", @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c4/Pok%C3%A9mon_Poison_Type_Icon.svg/{}px-Pok%C3%A9mon_Poison_Type_Icon.svg.png");
        public static readonly PokemonTypes Ground = new PokemonTypes("ground", "Ground", @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/Pok%C3%A9mon_Ground_Type_Icon.svg/{}px-Pok%C3%A9mon_Ground_Type_Icon.svg.png");
        public static readonly PokemonTypes Rock = new PokemonTypes("rock", "Rock", @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Pok%C3%A9mon_Rock_Type_Icon.svg/{}px-Pok%C3%A9mon_Rock_Type_Icon.svg.png");
        public static readonly PokemonTypes Bug = new PokemonTypes("bug", "Bug", @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/3c/Pok%C3%A9mon_Bug_Type_Icon.svg/{}px-Pok%C3%A9mon_Bug_Type_Icon.svg.png");
        public static readonly PokemonTypes Ghost = new PokemonTypes("ghost", "Ghost", @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/a0/Pok%C3%A9mon_Ghost_Type_Icon.svg/{}px-Pok%C3%A9mon_Ghost_Type_Icon.svg.png");
        public static readonly PokemonTypes Steel = new PokemonTypes("steel", "Steel", @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/Pok%C3%A9mon_Steel_Type_Icon.svg/{}px-Pok%C3%A9mon_Steel_Type_Icon.svg.png");
        public static readonly PokemonTypes Fire = new PokemonTypes("fire", "Fire", @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/56/Pok%C3%A9mon_Fire_Type_Icon.svg/{}px-Pok%C3%A9mon_Fire_Type_Icon.svg.png");
        public static readonly PokemonTypes Water = new PokemonTypes("water", "Water", @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Pok%C3%A9mon_Water_Type_Icon.svg/{}px-Pok%C3%A9mon_Water_Type_Icon.svg.png");
        public static readonly PokemonTypes Grass = new PokemonTypes("grass", "Grass", @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Pok%C3%A9mon_Grass_Type_Icon.svg/{}px-Pok%C3%A9mon_Grass_Type_Icon.svg.png");
        public static readonly PokemonTypes Electric = new PokemonTypes("electric", "Electric", @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Pok%C3%A9mon_Electric_Type_Icon.svg/{}px-Pok%C3%A9mon_Electric_Type_Icon.svg.png");
        public static readonly PokemonTypes Psychic = new PokemonTypes("psychic", "Psychic", @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/ab/Pok%C3%A9mon_Psychic_Type_Icon.svg/{}px-Pok%C3%A9mon_Psychic_Type_Icon.svg.png");
        public static readonly PokemonTypes Ice = new PokemonTypes("ice", "Ice", @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Pok%C3%A9mon_Ice_Type_Icon.svg/{}px-Pok%C3%A9mon_Ice_Type_Icon.svg.png");
        public static readonly PokemonTypes Dragon = new PokemonTypes("dragon", "Dragon", @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Pok%C3%A9mon_Dragon_Type_Icon.svg/{}px-Pok%C3%A9mon_Dragon_Type_Icon.svg.png");
        public static readonly PokemonTypes Dark = new PokemonTypes("dark", "Dark", @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Pok%C3%A9mon_Dark_Type_Icon.svg/{}px-Pok%C3%A9mon_Dark_Type_Icon.svg.png");
        public static readonly PokemonTypes Fairy = new PokemonTypes("fairy", "Fairy", @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/08/Pok%C3%A9mon_Fairy_Type_Icon.svg/{}px-Pok%C3%A9mon_Fairy_Type_Icon.svg.png");

        private PokemonTypes(string id, string value, string uri)
        {
            Id = id;
            DisplayValue = value;
            URI = uri;

            PokemonTypesCollection.Add(this);
        }

        public static PokemonTypes GetPokemonTypeById(string id)
        {
            return PokemonTypesCollection.First(x => x.Id == id.ToLower().Trim());
        }

        public string ImageURI(int size = 60)
        {
            return URI.Replace("{}", $"{size}");
        }
    }
}
