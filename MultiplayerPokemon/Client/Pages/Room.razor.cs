using Fluxor;
using Microsoft.AspNetCore.Components;
using MultiplayerPokemon.Client.Store.RoomUseCase;
using MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase;
using MultiplayerPokemon.Client.Store.UserUseCase;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Pages
{
    public partial class Room
    {
        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        private IState<RoomState> RoomState { get; set; }

        [Inject]
        private IState<ConnectionState> ConnectionState { get; set; }

        [Inject]
        private IState<UserState> UserState { get; set; }


        private string messageText = string.Empty;

        private async void HandleSendMessage()
        {
            if (Dispatcher is not null && RoomState.Value is not null && ConnectionState.Value?.Connection is not null)
            {
                await ConnectionState.Value.Connection.SendCoreAsync("SendMessageToRoom", new object[] { messageText, RoomState.Value.RoomName });
                Dispatcher.Dispatch(new AddMessageAction(new MessageModel
                {
                    MessageText = messageText,
                    User = UserState.Value.User,
                    SentDate = DateTime.Now
                }));
            }
        }
    }
}
