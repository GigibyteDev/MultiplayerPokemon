using MultiplayerPokemon.Server.Orchestrators.Interfaces;
using MultiplayerPokemon.Server.Repositories.Interfaces;
using MultiplayerPokemon.Shared.Dtos;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Server.Orchestrators
{
    public class RoomOrchestrator : IRoomOrchestrator
    {
        private readonly IRoomRepository roomRepository;
        private readonly IUserRepository userRepository;
        public RoomOrchestrator(IRoomRepository _roomRepository, IUserRepository _userRepository)
        {
            roomRepository = _roomRepository;
            userRepository = _userRepository;
        }
        public async Task<CreateRoomResult> CreateRoom(CreateRoomRequest request)
        {
            return await roomRepository.CreateRoom(request);
        }

        public async Task<RoomModel?> GetRoomByRoomName(string roomName)
        {
            return await roomRepository.GetRoomByRoomName(roomName);
        }

        public async Task<UserModel?> GetUserByUsername(string username)
        {
            var user = await userRepository.GetUserByUsername(username);
            if (user is not null)
                return new UserModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    IsAdmin = user.IsAdmin,
                };

            return null;
        }

        public async Task<bool> AddUserToRoom(string connectionId, UserModel user, string roomName)
        {
            return await roomRepository.AddUserToRoom(connectionId, user, roomName);
        }

        public async Task<RoomModel?> GetUserRoomIfConnected(UserModel user)
        {
            return await roomRepository.GetUserRoomIfConnected(user);
        }

        public async Task<bool> RemoveUserFromRoom(UserModel user, string roomName)
        {
            return await roomRepository.RemoveUserFromRoom(user, roomName);
        }

        public async Task<IEnumerable<string>> ForceRemoveUserFromAnyRoom(UserModel user)
        {
            return await roomRepository.ForceRemoveUserFromAnyRoom(user);
        }

        public async Task<bool> AddMessageToRoom(MessageModel message, string roomName)
        {
            return await roomRepository.AddMessageToRoom(message, roomName);
        }
    }
}
