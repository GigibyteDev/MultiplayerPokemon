namespace MultiplayerPokemon.Client.Models
{
    public class RoomPartyModel
    {
        public List<PartyCardModel> Cards = new List<PartyCardModel>();

        public RoomPartyModel(List<PartyCardModel> cards)
        {
            Cards = cards;
        }
    }
}
