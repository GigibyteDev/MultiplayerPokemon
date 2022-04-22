namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class SelectCardAction
    {
        public int CardId { get; set; }

        public SelectCardAction(int cardId)
        {
            CardId = cardId;
        }
    }
}
