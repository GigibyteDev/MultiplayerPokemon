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

        public async Task<bool> AddUserToRoom(UserModel user, string roomName)
        {
            return await roomRepository.AddUserToRoom(user, roomName);
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

        public async Task<IEnumerable<RoomData>> GetRoomListData()
        {
            return await roomRepository.GetRoomListData();
        }

        public async Task<bool> AddPokemonToParty(PokemonPartyDataModel partyModel, string roomName)
        {
            return await roomRepository.AddPokemonToParty(partyModel, roomName);
        }

        public async Task<bool> RemovePokemonFromParty(int position, string roomName)
        {
            return await roomRepository.RemovePokemonFromParty(position, roomName);
        }

        public async Task<bool> SwapPokemonInParty(int originalSpot, int newSpot, string roomName)
        {
            return await roomRepository.SwapPokemonInParty(originalSpot, newSpot, roomName);
        }
    }
}
