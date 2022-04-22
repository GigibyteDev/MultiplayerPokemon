using Microsoft.AspNetCore.SignalR;
using MultiplayerPokemon.Server.Attributes;
using MultiplayerPokemon.Server.Helpers;
using MultiplayerPokemon.Server.Orchestrators.Interfaces;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Server.Hubs
{
    [Authorize]
    public class RoomHub : Hub
    {
        private readonly IRoomOrchestrator roomOrchestrator;
        private readonly UserModel ChatBot;
        private readonly SignalRConnectionManager connectionManager;
        public RoomHub(IRoomOrchestrator _userOrchestrator, SignalRConnectionManager _connectionManager)
        {
            roomOrchestrator = _userOrchestrator;
            connectionManager = _connectionManager;
            ChatBot = new UserModel
            {
                IsAdmin = true,
                Username = "Chat Bot",
                Id = -1
            };
        }

        public async Task TestRoomHub(string username)
        {
            var user = await roomOrchestrator.GetUserByUsername(username);
            if (user is not null)
            {
                string? connectionToDisconnect = connectionManager.AddUserConnection(Context.ConnectionId, user);

                if (connectionToDisconnect is not null)
                {
                    await InvokeDisconnectUserFromSignalR(connectionToDisconnect);
                }

                await InvokeTestRoomHubConnection(user);

                var previouslyConnectedRoom = await roomOrchestrator.GetUserRoomIfConnected(user);
                if (previouslyConnectedRoom is not null)
                {
                    await SilentConnectToRoom(previouslyConnectedRoom.RoomName, Context.ConnectionId);
                }
            }
        }

        public async Task ConnectToRoom(string roomName)
        {
            if (connectionManager.CurrentlyConnectedUsers.TryGetValue(Context.ConnectionId, out UserModel? user))
            {
                var room = await roomOrchestrator.GetRoomByRoomName(roomName);
                if (room is not null)
                {
                    if (!room.CurrentUsers.Any(x => x.Id == user.Id))
                    {
                        var connectionMessage = new MessageModel { User = ChatBot, MessageText = $"User {user.Username} has joined the room", SentDate = DateTime.Now };
                        await roomOrchestrator.AddMessageToRoom(connectionMessage, roomName);
                        await roomOrchestrator.AddUserToRoom(user, roomName);
                        await InvokeGetConnectedRoomInfo(room, Context.ConnectionId);
                        foreach(var prevUserConnectionId in connectionManager.GetAllAssociatedConnectionIds(Context.ConnectionId))
                        {
                            await SilentConnectToRoom(roomName, prevUserConnectionId);
                        }
                        await InvokeNewUserConnectedToRoom(room, user, connectionMessage);
                    }
                    else
                    {
                        await SilentConnectToRoom(room.RoomName, Context.ConnectionId);
                    }
                }
                else
                {
                    await InvokeErrorMessage("Could Not Connect To Room");
                }
            }
        }

        public async Task SilentConnectToRoom(string roomName, string connectionId)
        {
            if (connectionManager.CurrentlyConnectedUsers.TryGetValue(connectionId, out UserModel? user))
            {
                var room = await roomOrchestrator.GetRoomByRoomName(roomName);
                if (room is not null)
                {
                    connectionManager.AddUserToRoomLookup(connectionId, roomName);
                    await Groups.AddToGroupAsync(connectionId, room.RoomName);
                    await InvokeGetConnectedRoomInfo(room, connectionId);
                }
                else
                {
                    await InvokeErrorMessage("Could Not Connect To Room");
                }
            }
        }

        public async Task SendMessageToRoom(string message, string roomName)
        {
            if (connectionManager.CurrentlyConnectedUsers.TryGetValue(Context.ConnectionId, out UserModel? user))
            {
                var room = await roomOrchestrator.GetRoomByRoomName(roomName);
                if (room is not null)
                {
                    var newMessage = new MessageModel
                    {
                        User = user,
                        MessageText = message,
                        SentDate = DateTime.Now
                    };

                    await roomOrchestrator.AddMessageToRoom(newMessage, roomName);
                    await InvokeMessageSentToRoom(newMessage, room.RoomName);
                }
                else
                {
                    await InvokeErrorMessage("Could Not Send Message");
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (connectionManager.CurrentlyConnectedUsers.TryGetValue(Context.ConnectionId, out UserModel? user))
            {
                bool anyOtherConnections = connectionManager.RemoveConnectionIdFromRoomLookup(Context.ConnectionId, out string roomName);
            }
            else
            {
                await InvokeErrorMessage("Could Not Disconnect User");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task UserDisconnectedRoom(string roomName)
        {
            if (connectionManager.CurrentlyConnectedUsers.TryGetValue(Context.ConnectionId, out UserModel? user))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
                bool userRemoved = await roomOrchestrator.RemoveUserFromRoom(user, roomName);
                bool userConnectionsRemove = connectionManager.RemoveUserFromRoom(roomName, user.Id);
                if (userRemoved)
                {
                    IEnumerable<string> associatedConnections = connectionManager.GetAllAssociatedConnectionIds(Context.ConnectionId) ?? new List<string>();
                    await InvokeDisconnectFromRoom(associatedConnections);
                    await HandleSendDisconnectMessage(user, roomName);
                }
            }
            else
            {
                await InvokeErrorMessage("Could Not Disconnect User");
            }
        }

        public async Task AddPokemonToParty(PokemonPartyDataModel partyData, string roomName)
        {
            if (connectionManager.CurrentlyConnectedUsers.TryGetValue(Context.ConnectionId, out UserModel? user))
            {
                await roomOrchestrator.AddPokemonToParty(partyData, roomName);
                await InvokePokemonAddedToParty(partyData, roomName);
            }
        }

        public async Task RemovePokemonFromParty(int position, string roomName)
        {
            await roomOrchestrator.RemovePokemonFromParty(position, roomName);
            await InvokeRemovePokemonFromParty(position, roomName);
        }

        public async Task RemoveMultiplePokemonFromParty(IEnumerable<int> positions, string roomName)
        {
            await roomOrchestrator.RemoveMultiplePokemonFromParty(positions, roomName);
            await InvokeRemoveMultiplePokemonFromParty(positions, roomName);
        }

        public async Task SwapPokemon(int currentPos, int newPos, string RoomName)
        {
            await roomOrchestrator.SwapPokemonInParty(currentPos, newPos, RoomName);
            await InvokePokemonSwapped(currentPos, newPos, RoomName);
        }

        private async Task InvokeRemovePokemonFromParty(int position, string roomName)
        {
            await Clients.OthersInGroup(roomName).SendAsync("RemovePokemonFromParty", position);
        }

        private async Task InvokeRemoveMultiplePokemonFromParty(IEnumerable<int> positions, string roomName)
        {
            await Clients.OthersInGroup(roomName).SendAsync("RemoveMultiplePokemonFromParty", positions);
        }

        private async Task InvokeDisconnectFromRoom(IEnumerable<string> connectionIds)
        {
            await Clients.Clients(connectionIds).SendAsync("DisconnectUserFromRoom");
        }

        private async Task InvokeDisconnectUserFromSignalR(string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("DisconnectUserFromSignalR");
        }

        private async Task HandleSendDisconnectMessage(UserModel user, string roomName)
        {
            var room = await roomOrchestrator.GetRoomByRoomName(roomName);
            if (room is not null)
            {
                var disconnectedMessage = new MessageModel
                {
                    User = ChatBot,
                    MessageText = $"User {user.Username} has disconnected",
                    SentDate = DateTime.Now
                };
                await roomOrchestrator.AddMessageToRoom(disconnectedMessage, roomName);
                await Clients.OthersInGroup(room.RoomName).SendAsync("UserDisconnectedFromRoom", user.Username, disconnectedMessage);
            }
        }

        private async Task InvokePokemonAddedToParty(PokemonPartyDataModel pokemonData, string roomName)
        {
            await Clients.OthersInGroup(roomName).SendAsync("PokemonAddedToParty", pokemonData);
        }

        private async Task InvokePokemonSwapped(int currentPos, int newPos, string roomName)
        {
            await Clients.OthersInGroup(roomName).SendAsync("PokemonSwapped", currentPos, newPos);
        }

        private async Task InvokeMessageSentToRoom(MessageModel message, string roomName)
        {
            await Clients.OthersInGroup(roomName).SendAsync("MessageSentToRoom", message);
        }

        private async Task InvokeGetConnectedRoomInfo(RoomModel room, string connectionId)
        {
            await Clients.Clients(connectionManager.GetAllAssociatedConnectionIds(connectionId)).SendAsync("GetConnectedRoomInfo", room);
        }

        private async Task InvokeNewUserConnectedToRoom(RoomModel room, UserModel user, MessageModel connectionMessage)
        {
            await Clients.GroupExcept(room.RoomName, connectionManager.GetAllAssociatedConnectionIds(Context.ConnectionId)).SendAsync("NewUserConnectedToRoom", user, connectionMessage);
        }

        private async Task InvokeTestRoomHubConnection(UserModel user)
        {
            await Clients.Clients(connectionManager.GetAllAssociatedConnectionIds(Context.ConnectionId)).SendAsync("TestRoomHubInvoke", $"Successfully associated username: {user.Username} with Connection Id: {Context.ConnectionId}");
        }

        private async Task InvokeErrorMessage(string error)
        {
            await Clients.Clients(connectionManager.GetAllAssociatedConnectionIds(Context.ConnectionId)).SendAsync("ThrowError", error);
        }
    }
}
