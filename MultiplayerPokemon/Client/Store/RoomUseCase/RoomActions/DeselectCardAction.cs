namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class DeselectCardAction
    {
        public int CardId { get; set; }

        public DeselectCardAction(int cardId)
        {
            CardId = cardId;
        }
    }
}
