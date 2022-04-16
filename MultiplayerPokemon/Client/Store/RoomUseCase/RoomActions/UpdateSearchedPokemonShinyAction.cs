namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class UpdateSearchedPokemonShinyAction
    {
        public bool IsShiny { get; set; }
        public UpdateSearchedPokemonShinyAction(bool isShiny)
        {
            IsShiny = isShiny;
        }
    }
}
