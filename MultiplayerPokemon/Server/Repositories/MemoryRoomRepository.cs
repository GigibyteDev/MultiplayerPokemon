using MultiplayerPokemon.Server.Repositories.Interfaces;
using MultiplayerPokemon.Shared.Dtos;
using MultiplayerPokemon.Shared.Logic;
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
                PokemonParty = new PartyModel()
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

        public async Task<bool> AddUserToRoom(UserModel user, string roomName)
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
                if (room.CurrentUsers.Count == 0)
                {
                    Rooms.Remove(room);
                }
                return true;
            }
            return false;
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

        public async Task<IEnumerable<RoomData>> GetRoomListData()
        {
            IList<RoomData> roomData = new List<RoomData>();

            foreach (RoomModel room in Rooms)
            {
                roomData.Add(new RoomData()
                {
                    DateCreated = room.DateCreated,
                    Name = room.RoomName,
                    UsersConnected = room.CurrentUsers.Count
                });
            }

            return roomData;
        }

        private bool GetRoomIfExists(string roomName, out RoomModel room)
        {
            var tempRoom = Rooms.FirstOrDefault(x => x.RoomName.ToLower().Trim() == roomName.ToLower().Trim());
            room = tempRoom ?? new RoomModel();
            return tempRoom is not null;
        }

        public async Task<bool> AddPokemonToParty(PokemonPartyDataModel partyModel, string roomName)
        {
            if (GetRoomIfExists(roomName, out RoomModel room))
            {
                return room.PokemonParty.Pokemon.AddToCollection(partyModel);
            }

            return false;
        }

        public async Task<bool> SwapPokemonInParty(int originalSpot, int newSpot, string roomName)
        {
            if (GetRoomIfExists(roomName, out RoomModel room))
            {
                return room.PokemonParty.Pokemon.Swap(originalSpot, newSpot);
            }

            return false;
        }

        public async Task<bool> RemovePokemonFromParty(int position, string roomName)
        {
            if (GetRoomIfExists(roomName, out RoomModel room))
            {
                return room.PokemonParty.Pokemon.RemoveFromCollection(position);
            }

            return false;
        }
    }
}
