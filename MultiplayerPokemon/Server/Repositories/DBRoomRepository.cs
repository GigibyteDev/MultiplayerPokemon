using MultiplayerPokemon.Server.Repositories.Interfaces;
using MultiplayerPokemon.Shared.Dtos;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Server.Repositories
{
    // Would be utilized to store room information in a persistant database
    public class DBRoomRepository : IRoomRepository
    {
        public Task<bool> AddMessageToRoom(MessageModel message, string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserToRoom(string connectionId, UserModel user, string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<CreateRoomResult> CreateRoom(CreateRoomRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> ForceRemoveUserFromAnyRoom(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<RoomModel?> GetRoomByRoomName(string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<RoomModel?> GetUserRoomIfConnected(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRoom(UserModel user, string roomName)
        {
            throw new NotImplementedException();
        }
    }
}
