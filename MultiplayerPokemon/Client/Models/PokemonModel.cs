using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Models
{
    public class PokemonModel
    {
        public int Id { get; }
        public string Name { get; private set; }
        public PokemonSpriteCollection Sprites { get; private set; }
        public List<PokemonStat> Stats { get; private set; }
        public List<PokemonTypes> Types { get; private set; }
        public float Weight { get; private set; }
        public float Height { get; private set; }
        public bool CanAddToParty { get; private set; }
        public List<PokemonFlavorText> FlavorTexts { get; private set; }
        public PokemonGenderTypes GenderType { get; private set; }
        public List<PokemonAlt> Alts { get; private set; }

        public PokemonModel
        (
            int id,
            string? name,
            List<PokemonStat> stats,
            PokemonGenderTypes genderType,
            PokemonSpriteCollection sprites,
            List<PokemonTypes> types,
            float weight,
            float height,
            bool canAddToParty,
            List<PokemonFlavorText> flavorTexts,
            List<PokemonAlt> alts
        )
        {
            Id = id;
            Name = name ?? string.Empty;
            Stats = stats;
            GenderType = genderType;
            Sprites = sprites;
            Types = types;
            Weight = weight;
            Height = height;
            CanAddToParty = canAddToParty;
            FlavorTexts = flavorTexts;
            Alts = alts;
        }

        public void UpdateFromAlt(PokemonAltInformation alt)
        {
            Name = alt.Name;
            Sprites = new PokemonSpriteCollection
                (
                Sprites.OfficialArtwork,
                alt.Sprites.DefaultSprites,
                alt.Sprites.ShinySprites,
                alt.Sprites.FemaleSprites,
                alt.Sprites.ShinyFemaleSprites
                );
            Types = alt.Types;
        }
    }

    public class PokemonSpriteCollection
    {
        public PokemonSprites DefaultSprites { get; }
        public PokemonSprites ShinySprites { get; }
        public PokemonSprites FemaleSprites { get; }
        public PokemonSprites ShinyFemaleSprites { get; }
        public string OfficialArtwork { get; }

        public PokemonSpriteCollection(string officialArtwork, PokemonSprites defaultSprites, PokemonSprites shinySprites, PokemonSprites femaleSprites, PokemonSprites shinyFemaleSprites)
        {
            DefaultSprites = defaultSprites;
            ShinySprites = shinySprites;
            FemaleSprites = femaleSprites;
            ShinyFemaleSprites = shinyFemaleSprites;
            OfficialArtwork = officialArtwork;
        }
    }

    public class PokemonSprites
    {
        public string Front { get; set; }
        public string Back { get; set; }

        public PokemonSprites(string? front, string? back)
        {
            Front = front ?? string.Empty;
            Back = back ?? string.Empty;
        }
    }

    public class PokemonAltInformation
    {
        public string Name { get; set; }
        public PokemonSpriteCollection Sprites { get; set; }
        public List<PokemonTypes> Types { get; set; }

        public PokemonAltInformation(string name, PokemonSpriteCollection sprites, List<PokemonTypes> types)
        {
            Name = name;
            Sprites = sprites;
            Types = types;
        }
    }

    public class PokemonFlavorText
    {
        public string FlavorText { get; }
        public string Version { get; }

        public PokemonFlavorText(string flavorText, string version)
        {
            FlavorText = flavorText;
            Version = version;
        }
    }

    public class PokemonStat
    {
        public int Base { get; }
        public string Name { get; }

        public PokemonStat(int @base, string name)
        {
            Base = @base;
            Name = name;
        }
    }

    public class PokemonAlt
    {
        public string Name { get; }
        public string Link { get; }
        public PokemonAltOptions Type { get; }

        public PokemonAlt(string name, string link, PokemonAltOptions type)
        {
            Name = name;
            Link = link;
            Type = type;
        }
    }
}
