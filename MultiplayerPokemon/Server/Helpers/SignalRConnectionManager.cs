using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Server.Helpers
{
    public class SignalRConnectionManager
    {
        public IDictionary<string, UserModel> CurrentlyConnectedUsers { get; set; }
        private IDictionary<string, List<UserConnectionLookup>> UserConnectionsPerRoom { get; set; }

        public SignalRConnectionManager()
        {
            CurrentlyConnectedUsers = new Dictionary<string, UserModel>();
            UserConnectionsPerRoom = new Dictionary<string, List<UserConnectionLookup>>();
        }

        public IEnumerable<string> GetConnectionIds(IEnumerable<UserModel> userModels)
        {
            return CurrentlyConnectedUsers.Where(ccu => userModels.Select(um => um.Id).Contains(ccu.Value.Id)).Select(ccu => ccu.Key);
        }

        public string? AddUserConnection(string connectionId, UserModel user)
        {
            if (!CurrentlyConnectedUsers.ContainsKey(connectionId))
            {
                CurrentlyConnectedUsers.Add(connectionId, user);
                
                if (CurrentlyConnectedUsers.Where(x => x.Value.Id == user.Id).Count() > 3)
                {
                    return CurrentlyConnectedUsers.First(x => x.Value.Id == user.Id).Key;
                }
            }
            return null;
        }

        public bool RemoveUserConnection(string connectionId)
        {
            if (CurrentlyConnectedUsers.ContainsKey(connectionId))
            {
                CurrentlyConnectedUsers.Remove(connectionId);
                return true;
            }
            return false;
        }

        public bool AddUserToRoomLookup(string connectionId, string roomName)
        {
            if (CurrentlyConnectedUsers.TryGetValue(connectionId, out UserModel? user))
            {
                if (UserConnectionsPerRoom.ContainsKey(roomName))
                {
                    if (UserConnectionsPerRoom[roomName].Any(uc => uc.UserId == user.Id))
                    {
                        UserConnectionsPerRoom[roomName].Single(r => r.UserId == user.Id).ConnectionIds.Add(connectionId);
                    }
                    else
                    {
                        UserConnectionsPerRoom[roomName].Add(new UserConnectionLookup { UserId = user.Id, ConnectionIds = new List<string>() { connectionId } });
                    }
                }
                else
                {
                    UserConnectionsPerRoom.Add(roomName, new List<UserConnectionLookup>()
                    {
                        new UserConnectionLookup { UserId = user.Id, ConnectionIds = new List<string>() { connectionId } }
                    });
                }

                return true;
            }

            return false;
        }

        public bool RemoveConnectionIdFromRoomLookup(string connectionId, string roomName)
        {
            if (CurrentlyConnectedUsers.TryGetValue(connectionId, out UserModel? user))
            {
                bool anyRemainingConnections = true;
                if (UserConnectionsPerRoom.ContainsKey(roomName))
                {
                    UserConnectionsPerRoom[roomName].Single(r => r.UserId == user.Id).ConnectionIds.RemoveAll(c => c == connectionId);
                    anyRemainingConnections = UserConnectionsPerRoom[roomName].Single(r => r.UserId == user.Id).ConnectionIds.Any();
                    if (!anyRemainingConnections)
                    {
                        UserConnectionsPerRoom[roomName].RemoveAll(r => r.UserId == user.Id);
                        if (!UserConnectionsPerRoom[roomName].Any())
                        {
                            UserConnectionsPerRoom.Remove(roomName);
                        }
                    }
                }

                CurrentlyConnectedUsers.Remove(connectionId);
                return anyRemainingConnections;
            }

            return false;
        }

        public bool RemoveConnectionIdFromRoomLookup(string connectionId, out string roomName)
        {
            if (CurrentlyConnectedUsers.TryGetValue(connectionId, out UserModel? user))
            {
                roomName = GetUsersConnectedRoom(user.Id);

                return RemoveConnectionIdFromRoomLookup(connectionId, roomName);
            }

            roomName = string.Empty;
            return false;
        }

        public string GetUsersConnectedRoom(int userId)
        {
            return UserConnectionsPerRoom.FirstOrDefault(room => room.Value.Any(ucpp => ucpp.UserId == userId)).Key;
        }

        public IEnumerable<string> GetAllAssociatedConnectionIds(string connectionId)
        {
            if (CurrentlyConnectedUsers.TryGetValue(connectionId, out UserModel? user))
            {
                var associatedConnectionIds = CurrentlyConnectedUsers.Where(c => c.Value.Id == user.Id);
                List<string> ids = new List<string>();
                foreach(var id in associatedConnectionIds)
                {
                    ids.Add(id.Key);
                }

                return ids;
            }
            return new List<string>() { connectionId };
        }

        public bool RemoveUserFromRoom(string roomName, int userId)
        {
            if (UserConnectionsPerRoom.TryGetValue(roomName, out List<UserConnectionLookup>? userConnections))
            {
                userConnections.RemoveAll(uc => uc.UserId == userId);
                return true;
            }
            return false;
        }

        
        private class UserConnectionLookup
        {
            public int UserId { get; set; }
            public List<string> ConnectionIds { get; set; }
        }
    }
}
