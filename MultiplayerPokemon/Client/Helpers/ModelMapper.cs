using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Client.Models.DataModels;
using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Helpers
{
    public static class ModelMapper
    {
        public static PokemonModel MapRawPokemonDataToPokemonModel(this PokemonData pokemonData, PokemonSpeciesData speciesData, PokemonFormData formData)
        {
            List<PokemonAlt> forms = new List<PokemonAlt>();

            foreach (var variety in speciesData.Varieties ?? new List<PokemonSpeciesDataVariety>())
            {
                forms.Add(new PokemonAlt(
                    name: variety.Pokemon?.Name ?? string.Empty,
                    link: variety.Pokemon?.Url ?? string.Empty,
                    type: PokemonAltOptions.Variety
                    ));
            }

            foreach (var form in pokemonData.Forms ?? new List<PokemonDataForm>())
            {
                forms.Add(new PokemonAlt(
                    name: form.Name?.Replace("-normal", "") ?? string.Empty,
                    link: form.Url?.Where(c => char.IsDigit(c)).ToString() ?? string.Empty,
                    type: PokemonAltOptions.Form
                    ));
            }

            forms.RemoveAll(form => form.Name == pokemonData.Name && form.Type == PokemonAltOptions.Form);

            List<PokemonFlavorText> flavorTexts = new List<PokemonFlavorText>();

            foreach (var text in speciesData.FlavorTextEntries ?? new List<PokemonSpeciesDataFlavorText>())
            {
                if (text.Language?.Name == "en")
                {
                    flavorTexts.Add(new PokemonFlavorText(
                        flavorText: text.FlavorText ?? string.Empty,
                        version: text.Version?.Name ?? string.Empty
                        ));
                }
            }

            PokemonGenderTypes genderType;

            switch (speciesData.GenderRate)
            {
                case -1:
                    genderType = PokemonGenderTypes.Genderless;
                    break;
                case 0:
                    genderType = PokemonGenderTypes.MaleOnly;
                    break;
                case 8:
                    genderType = PokemonGenderTypes.FemaleOnly;
                    break;
                default:
                    genderType = PokemonGenderTypes.MaleOrFemale;
                    break;
            }

            return new PokemonModel
               (
                   id: pokemonData.Id,
                   name: pokemonData.Name,
                   sprites: AssignSprites(pokemonData.Sprites ?? new PokemonDataSprites()),
                   stats: pokemonData.Stats.GetPokemonStats(),
                   types: pokemonData.Types.GetPokemonTypes(),
                   weight: pokemonData.Weight,
                   height: pokemonData.Height,
                   canAddToParty: !formData.IsBattleOnly,
                   flavorTexts: flavorTexts,
                   genderType: genderType,
                   alts: forms
               );
        }

        public static PokemonAltInformation MapPokemonFormDataToAltInformation(this PokemonFormData formData)
        {
            List<PokemonTypes> types = new List<PokemonTypes>();

            foreach (var type in formData.Types ?? new List<PokemonDataType>())
            {
                types.Add(PokemonTypes.GetPokemonTypeById(type.Type?.Name ?? string.Empty));
            }

            var altInfoSprites = AssignSprites(formData.Sprites);

            return new PokemonAltInformation
                (
                    formData.Name ?? string.Empty,
                    altInfoSprites,
                    types
                );
        }

        public static PartyCardModel MapPokemonDataToPartyCardModel(this PokemonData pokemonData, string gender, bool isShiny)
        {
            return new PartyCardModel
            {
                Id = pokemonData.Id,
                Name = pokemonData.Name ?? string.Empty,
                Gender = gender,
                IsShiny = isShiny,
                ImageURI = pokemonData.Sprites?.Other?.OfficialArtWork?.FrontDefault ?? string.Empty,
                Stats = pokemonData.Stats.GetPokemonStats(),
                Types = pokemonData.Types.GetPokemonTypes()
            };
        }

        private static List<PokemonStat> GetPokemonStats(this IEnumerable<PokemonDataStat>? dataStats)
        {
            List<PokemonStat> stats = new List<PokemonStat>();

            foreach (var stat in dataStats ?? new List<PokemonDataStat>())
            {
                stats.Add(new PokemonStat
                (
                    @base: stat.StatBase,
                    name: stat.StatName?.Name?.Replace("special-", "sp ")?.Replace("attack", "atk")?.Replace("defense", "def") ?? string.Empty
                ));
            }

            return stats;
        }

        private static List<PokemonTypes> GetPokemonTypes(this IEnumerable<PokemonDataType>? dataTypes)
        {
            List<PokemonTypes> types = new List<PokemonTypes>();

            foreach (var type in dataTypes ?? new List<PokemonDataType>())
            {
                types.Add(PokemonTypes.GetPokemonTypeById(type.Type?.Name ?? string.Empty));
            }

            return types;
        }

        private static PokemonSpriteCollection AssignSprites(PokemonDataSprites sprites, string? originalDefaultSprite = null)
        {
            PokemonSprites defaultSprites = new PokemonSprites
                (
                    front: sprites.FrontDefault,
                    back: sprites.BackDefault
                );

            PokemonSprites shinySprites = new PokemonSprites
                (
                    front: sprites.FrontShiny,
                    back: sprites.BackShiny
                );

            PokemonSprites femaleSprites = new PokemonSprites
                (
                    front: !string.IsNullOrWhiteSpace(sprites.FrontFemale) ? sprites.FrontFemale : sprites.FrontDefault,
                    back: !string.IsNullOrWhiteSpace(sprites.BackFemale) ? sprites.BackFemale : sprites.BackDefault
                );

            PokemonSprites femaleShinySprites = new PokemonSprites
                (
                    front: !string.IsNullOrWhiteSpace(sprites.FrontShinyFemale) ? sprites.FrontShinyFemale : sprites.FrontShiny,
                    back: !string.IsNullOrWhiteSpace(sprites.BackShinyFemale) ? sprites.BackShinyFemale : sprites.BackShiny
                );

            string officialArtwork = originalDefaultSprite ?? sprites.Other?.OfficialArtWork?.FrontDefault ?? string.Empty;

            return new PokemonSpriteCollection
            (
                officialArtwork: officialArtwork,
                defaultSprites: defaultSprites,
                shinySprites: shinySprites,
                femaleSprites: femaleSprites,
                shinyFemaleSprites: femaleShinySprites
            );
        }
    }
}
