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
                Username = "Chat Bot"
            };
        }

        public async Task TestRoomHub(string username)
        {
            var user = await roomOrchestrator.GetUserByUsername(username);
            if (user is not null)
            {
                connectionManager.AddUserConnection(Context.ConnectionId, user);
                await InvokeTestRoomHubConnection(user);

                var previouslyConnectedRoom = connectionManager.GetUsersConnectedRoom(user.Id);
                if (!string.IsNullOrWhiteSpace(previouslyConnectedRoom))
                {
                    await SilentConnectToRoom(previouslyConnectedRoom);
                }
            }
        }

        public async Task ConnectToRoom(string roomName)
        {
            if (connectionManager.CurrentlyConnectedUsers.TryGetValue(Context.ConnectionId, out UserModel? user))
            {
                var room = await roomOrchestrator.GetRoomByRoomName(roomName);
                if (room is not null && !room.CurrentUsers.Any(c => c.Username == user.Username))
                {
                    var connectionMessage = new MessageModel { User = ChatBot, MessageText = $"User {user.Username} has joined the room", SentDate = DateTime.Now };
                    await roomOrchestrator.AddMessageToRoom(connectionMessage, roomName);
                    foreach(var prevUserConnectionId in connectionManager.GetAllAssociatedConnectionIds(Context.ConnectionId))
                    {
                        connectionManager.AddUserToRoomLookup(prevUserConnectionId, roomName);
                        await Groups.AddToGroupAsync(prevUserConnectionId, room.RoomName);
                    }
                    await roomOrchestrator.AddUserToRoom(Context.ConnectionId, user, roomName);
                    await InvokeGetConnectedRoomInfo(room);
                    await InvokeNewUserConnectedToRoom(room, user, connectionMessage);
                }
                else
                {
                    await InvokeErrorMessage("Could Not Connect To Room");
                }
            }
        }

        public async Task SilentConnectToRoom(string roomName)
        {
            if (connectionManager.CurrentlyConnectedUsers.TryGetValue(Context.ConnectionId, out UserModel? user))
            {
                var room = await roomOrchestrator.GetRoomByRoomName(roomName);
                if (room is not null)
                {
                    connectionManager.AddUserToRoomLookup(Context.ConnectionId, roomName);
                    await Groups.AddToGroupAsync(Context.ConnectionId, room.RoomName);
                    await InvokeGetConnectedRoomInfo(room);
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
                    await InvokeMessageSentToRoom(newMessage, room);
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
                bool noOtherConnections = connectionManager.RemoveConnectionIdFromRoomLookup(Context.ConnectionId, out string roomName);
                if (noOtherConnections)
                {
                    var roomsRemoved = await roomOrchestrator.RemoveUserFromRoom(user, roomName);
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
                    await HandleSendDisconnectMessage(user, roomName);
                }
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
                bool noOtherConnections = connectionManager.RemoveConnectionIdFromRoomLookup(Context.ConnectionId, roomName);
                if (noOtherConnections)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
                    bool userRemoved = await roomOrchestrator.RemoveUserFromRoom(user, roomName);
                    if (userRemoved)
                    {
                        await HandleSendDisconnectMessage(user, roomName);
                    }
                }
            }
            else
            {
                await InvokeErrorMessage("Could Not Disconnect User");
            }
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

        private async Task InvokeMessageSentToRoom(MessageModel message, RoomModel room)
        {
            await Clients.OthersInGroup(room.RoomName).SendAsync("MessageSentToRoom", message);
        }

        private async Task InvokeGetConnectedRoomInfo(RoomModel room)
        {
            await Clients.Clients(connectionManager.GetAllAssociatedConnectionIds(Context.ConnectionId)).SendAsync("GetConnectedRoomInfo", room);
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
