using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Shared.Dtos
{
    public class CreateRoomResult
    {
        public bool Success { get; set; }
        public string RoomName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
