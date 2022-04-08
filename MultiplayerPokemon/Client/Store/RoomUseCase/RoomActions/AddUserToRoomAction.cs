using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class AddUserToRoomAction
    {
        public UserModel User { get; set; }
        public MessageModel ConnectionMessage { get; set; }

        public AddUserToRoomAction(UserModel user, MessageModel connectionMessage)
        {
            User = user;
            ConnectionMessage = connectionMessage;
        }
    }
}
