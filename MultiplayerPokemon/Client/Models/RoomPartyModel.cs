namespace MultiplayerPokemon.Client.Models
{
    public class RoomPartyModel
    {
        public Dictionary<int, PartyCardModel> Cards = new Dictionary<int, PartyCardModel>();

        public RoomPartyModel(Dictionary<int, PartyCardModel> cards)
        {
            Cards = cards;
        }
    }
}
