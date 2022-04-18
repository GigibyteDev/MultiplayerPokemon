using MultiplayerPokemon.Shared.Dtos;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Server.Orchestrators.Interfaces
{
    public interface IRoomOrchestrator
    {
        Task<CreateRoomResult> CreateRoom(CreateRoomRequest request);
        Task<RoomModel?> GetRoomByRoomName(string roomName);
        Task<UserModel?> GetUserByUsername(string username);
        Task<bool> AddUserToRoom(UserModel user, string roomName);
        Task<RoomModel?> GetUserRoomIfConnected(UserModel user);
        Task<bool> RemoveUserFromRoom(UserModel user, string roomName);
        Task<bool> AddMessageToRoom(MessageModel message, string roomName);
        Task<IEnumerable<RoomData>> GetRoomListData();
        Task<bool> AddPokemonToParty(PokemonPartyDataModel partyModel, string roomName);
        Task<bool> RemovePokemonFromParty(int position, string roomName);
        Task<bool> SwapPokemonInParty(int originalSpot, int newSpot, string roomName);
    }
}
