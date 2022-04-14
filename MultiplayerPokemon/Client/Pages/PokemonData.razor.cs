using Fluxor;
using Microsoft.AspNetCore.Components;
using MultiplayerPokemon.Client.Clients;
using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class PokemonData
    {
        [Parameter]
        public PokemonModel? PokemonModel { get; set; }

        [Parameter]
        public Action<string?>? GetPokemon { get; set; }

        private string pokemonAltDropdown = string.Empty;

        private string PokemonAltDropdown
        {
            get => pokemonAltDropdown;

            set
            {
                pokemonAltDropdown = value;
                UpdatePokemon(value);
            }
        }

        [Inject]
        private RESTPokemonClient? PokemonClient { get; set; }

        [Inject]
        private IDispatcher? Dispatcher { get; set; }

        private string Gender { get; set; } = "male";
        private bool IsFront { get; set; } = true;
        private bool IsShiny { get; set; } = false;
        private string ImgUri { get; set; } = string.Empty;
        private string PokemonSelectedDescription { get; set; }
        private string pokemonSelectedGameValue = string.Empty;
        private string PokemonSelectedGame
        {
            get
            {
                return pokemonSelectedGameValue;
            }

            set
            {
                pokemonSelectedGameValue = value;
                SetDescription();
            }
        }

        private  bool CanFlip
        {
            get => !string.IsNullOrWhiteSpace(PokemonModel?.Sprites.DefaultSprites.Back);
        }

        private void HandleShinyChange()
        {
            IsShiny = !IsShiny;
            UpdateImageURI();
        }

        private void SetDescription()
        {
            if (PokemonModel is not null)
                PokemonSelectedDescription = PokemonModel.FlavorTexts.Where(texts => texts.Version == pokemonSelectedGameValue).First().FlavorText;
        }

        private async void HandleFlipSet()
        {
            if (CanFlip)
            {
                IsFront = !IsFront;
                UpdateImageURI();

                // await JS.InvokeVoidAsync("add_animation", "pokemonSprite", "bounce");
            }
        }

        private void HandleGenderChange()
        {
            if (PokemonModel?.GenderType == PokemonGenderTypes.MaleOrFemale)
            {
                if (Gender == "male")
                {
                    Gender = "female";
                }
                else
                {
                    Gender = "male";
                }
                UpdateImageURI();
            }
        }

        private void HandleGenderSet()
        {
            switch (PokemonModel?.GenderType)
            {
                case PokemonGenderTypes.MaleOrFemale:
                    if (Gender != "male" && Gender != "female")
                    {
                        Gender = "male";
                    }
                    break;
                case PokemonGenderTypes.MaleOnly:
                    Gender = "male";
                    break;
                case PokemonGenderTypes.FemaleOnly:
                    Gender = "female";
                    break;
                default:
                    Gender = "genderless";
                    break;
            }
        }

        private void UpdateImageURI()
        {
            if (PokemonModel is not null)
                if (IsShiny)
                {
                    if (Gender == "male")
                    {
                        if (IsFront)
                        {
                            ImgUri = PokemonModel.Sprites.ShinySprites.Front;
                        }
                        else
                        {
                            ImgUri = PokemonModel.Sprites.ShinySprites.Back;
                        }
                    }
                    else
                    {
                        if (IsFront)
                        {
                            ImgUri = PokemonModel.Sprites.ShinyFemaleSprites.Front;
                        }
                        else
                        {
                            ImgUri = PokemonModel.Sprites.ShinyFemaleSprites.Back;
                        }
                    }
                }
                else
                {
                    if (Gender == "male")
                    {
                        if (IsFront)
                        {
                            ImgUri = PokemonModel.Sprites.DefaultSprites.Front;
                        }
                        else
                        {
                            ImgUri = PokemonModel.Sprites.DefaultSprites.Back;
                        }
                    }
                    else
                    {
                        if (IsFront)
                        {
                            ImgUri = PokemonModel.Sprites.FemaleSprites.Front;
                        }
                        else
                        {
                            ImgUri = PokemonModel.Sprites.FemaleSprites.Back;
                        }
                    }
                }

            StateHasChanged();
        }

        private void GetNextPokemon(string nextOrPrevious)
        {
            if (GetPokemon is not null && PokemonModel is not null)
                if (nextOrPrevious == "next")
                {
                    GetPokemon((PokemonModel.Id + 1).ToString());
                }
                else if (nextOrPrevious == "previous")
                {
                    GetPokemon((PokemonModel.Id - 1).ToString());
                }
        }

        private async void UpdatePokemon(string pokemonName)
        {
            if (PokemonModel?.Alts.First(alt => alt.Name == pokemonName).Type == PokemonAltOptions.Form)
            {
                if (PokemonClient is not null)
                {
                    PokemonAltInformation? pokemonAltInfo = await PokemonClient.GetPokemonAlt(pokemonName);
                    if (pokemonAltInfo != null)
                    {
                        PokemonModel.UpdateFromAlt(pokemonAltInfo);
                        UpdateImageURI();
                        StateHasChanged();
                    }
                }
            }
            else
            {
                if (GetPokemon is not null)
                    GetPokemon(pokemonName);
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            HandleGenderSet();

            if (!CanFlip)
            {
                IsFront = true;
            }

            UpdateImageURI();

            pokemonAltDropdown = PokemonModel?.Name ?? string.Empty;

            if (PokemonModel is not null)
                if (!string.IsNullOrWhiteSpace(PokemonSelectedGame) && PokemonModel.FlavorTexts.Any(text => text.Version == PokemonSelectedGame))
                {
                    SetDescription();
                }
                else
                {
                    PokemonSelectedGame = PokemonModel.FlavorTexts.Last().Version;
                }

            StateHasChanged();
        }
    }
}
