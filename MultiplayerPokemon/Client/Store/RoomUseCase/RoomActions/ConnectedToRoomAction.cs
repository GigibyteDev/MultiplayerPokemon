using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class ConnectedToRoomAction
    {
        public RoomModel RoomModel { get; set; }

        public RoomPartyModel PartyModel { get; set; }

        public ConnectedToRoomAction(RoomModel roomModel, RoomPartyModel partyModel)
        {
            RoomModel = roomModel;
            PartyModel = partyModel;
        }
    }
}
