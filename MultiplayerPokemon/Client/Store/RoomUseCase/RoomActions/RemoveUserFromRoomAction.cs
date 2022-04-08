using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class RemoveUserFromRoomAction
    {
        public string Username { get; set; }
        public MessageModel DisconnectMessage { get; set; }

        public RemoveUserFromRoomAction(string username, MessageModel disconnectedMessage)
        {
            Username = username;
            DisconnectMessage = disconnectedMessage;
        }
    }
}
