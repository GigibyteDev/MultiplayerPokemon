namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class RemovePokemonFromPartyAction
    {
        public int CardId { get; set; }

        public RemovePokemonFromPartyAction(int cardId)
        {
            CardId = cardId;
        }
    }
}
