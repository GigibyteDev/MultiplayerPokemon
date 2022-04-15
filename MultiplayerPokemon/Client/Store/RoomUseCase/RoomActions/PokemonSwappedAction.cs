namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    
    public class PokemonSwappedAction
    {
        public int CurrentPosition { get; set; }
        public int NewPosition { get; set; }
        public PokemonSwappedAction(int currentPosition, int newPosition)
        {
            CurrentPosition = currentPosition;
            NewPosition = newPosition;
        }
    }
}
