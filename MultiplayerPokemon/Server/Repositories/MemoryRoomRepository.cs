using MultiplayerPokemon.Server.Repositories.Interfaces;
using MultiplayerPokemon.Shared.Dtos;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Server.Repositories
{
    public class MemoryRoomRepository : IRoomRepository
    {
        private readonly List<RoomModel> Rooms;
        public MemoryRoomRepository()
        {
            Rooms = new List<RoomModel>();
        }
        public async Task<CreateRoomResult> CreateRoom(CreateRoomRequest request)
        {
            if (GetRoomIfExists(request.RoomName, out RoomModel room))
            {
                return new CreateRoomResult
                {
                    Success = false,
                    ErrorMessage = "Room Already Exists!"
                };
            }
            var newRoom = new RoomModel
            {
                RoomName = request.RoomName,
                CreatorUsername = request.Username,
                ModUsernames = new List<string> { request.Username },
                Chat = new ChatModel() { Messages = new List<MessageModel>() },
                CurrentUsers = new List<UserModel>(),
                UserHistory = new List<UserModel>(),
                PokemonParty = new PartyModel { Pokemon = new List<PokemonPartyDataModel>() }
            };

            Rooms.Add(newRoom);

            return new CreateRoomResult
            {
                Success = true,
                RoomName = request.RoomName,
            };
        }

        public async Task<RoomModel?> GetRoomByRoomName(string roomName)
        {
            if (GetRoomIfExists(roomName, out RoomModel room))
            {
                return room;
            }

            return null;
        }

        public async Task<bool> AddUserToRoom(string connectionId, UserModel user, string roomName)
        {
            if (GetRoomIfExists(roomName, out RoomModel room))
            {
                room.CurrentUsers.Add(user);
                room.UserHistory.Add(user);
                return true;
            }

            return false;
        }

        public async Task<RoomModel?> GetUserRoomIfConnected(UserModel user)
        {
            return Rooms.FirstOrDefault(r => r.CurrentUsers.Any(u => u.Username == user.Username));
        }

        public async Task<bool> RemoveUserFromRoom(UserModel user, string roomName)
        {
            if (GetRoomIfExists(roomName, out RoomModel room))
            {
                room.CurrentUsers.RemoveAll(u => u.Id == user.Id);

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<string>> ForceRemoveUserFromAnyRoom(UserModel user)
        {
            var userRooms = Rooms.Where(r => r.CurrentUsers.Any(v => v.Username == user.Username));

            if (userRooms.Any())
            {
                List<string> activeRoomNames = new List<string>();
                foreach (var userRoom in userRooms)
                {
                    userRoom.CurrentUsers.RemoveAll(u => u.Id == user.Id);
                    activeRoomNames.Add(userRoom.RoomName);
                }

                return activeRoomNames;
            }

            return new List<string>();
        }

        public async Task<bool> AddMessageToRoom(MessageModel message, string roomName)
        {
            if (GetRoomIfExists(roomName, out RoomModel room))
            {
                room.Chat.Messages.Add(message);
                return true;
            }

            return false;
        }

        private bool GetRoomIfExists(string roomName, out RoomModel room)
        {
            var tempRoom = Rooms.FirstOrDefault(x => x.RoomName.ToLower().Trim() == roomName.ToLower().Trim());
            room = tempRoom ?? new RoomModel();
            return tempRoom is not null;
        }
    }
}
