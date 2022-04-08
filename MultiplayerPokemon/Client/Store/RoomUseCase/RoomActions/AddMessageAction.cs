using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class AddMessageAction
    {
        public MessageModel Message { get; set; }

        public AddMessageAction(MessageModel message)
        {
            Message = message;
        }
    }
}
