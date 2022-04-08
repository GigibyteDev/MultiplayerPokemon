using MultiplayerPokemon.Shared.Dtos;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Server.Orchestrators.Interfaces
{
    public interface IRoomOrchestrator
    {
        Task<CreateRoomResult> CreateRoom(CreateRoomRequest request);
        Task<RoomModel?> GetRoomByRoomName(string roomName);
        Task<UserModel?> GetUserByUsername(string username);
        Task<bool> AddUserToRoom(string connectionId, UserModel user, string roomName);
        Task<RoomModel?> GetUserRoomIfConnected(UserModel user);
        Task<bool> RemoveUserFromRoom(UserModel user, string roomName);
        Task<IEnumerable<string>> ForceRemoveUserFromAnyRoom(UserModel user);
        Task<bool> AddMessageToRoom(MessageModel message, string roomName);
    }
}
