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

        public Task<bool> AddPokemonToParty(PokemonPartyDataModel partyModel, string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserToRoom(UserModel user, string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<CreateRoomResult> CreateRoom(CreateRoomRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RoomModel?> GetRoomByRoomName(string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoomData>> GetRoomListData()
        {
            throw new NotImplementedException();
        }

        public Task<RoomModel?> GetUserRoomIfConnected(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveMultiplePokemonFromParty(IEnumerable<int> positions, string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemovePokemonFromParty(int position, string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRoom(UserModel user, string roomName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SwapPokemonInParty(int originalSpot, int newSpot, string roomName)
        {
            throw new NotImplementedException();
        }
    }
}
