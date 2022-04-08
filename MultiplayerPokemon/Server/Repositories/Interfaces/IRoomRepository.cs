using MultiplayerPokemon.Shared.Dtos;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Server.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<CreateRoomResult> CreateRoom(CreateRoomRequest request);
        Task<RoomModel?> GetRoomByRoomName(string roomName);
        Task<bool> AddUserToRoom(string connectionId, UserModel user, string roomName);
        Task<RoomModel?> GetUserRoomIfConnected(UserModel user);
        Task<bool> RemoveUserFromRoom(UserModel user, string roomName);
        Task<IEnumerable<string>> ForceRemoveUserFromAnyRoom(UserModel user);
        Task<bool> AddMessageToRoom(MessageModel message, string roomName);
    }
}
