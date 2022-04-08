using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class ConnectedToRoomAction
    {
        public RoomModel RoomModel { get; set; }

        public ConnectedToRoomAction(RoomModel roomModel)
        {
            RoomModel = roomModel;
        }
    }
}
