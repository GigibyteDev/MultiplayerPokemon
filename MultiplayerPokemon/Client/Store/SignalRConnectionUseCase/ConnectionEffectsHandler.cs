using Fluxor;
using Microsoft.AspNetCore.SignalR.Client;
using MultiplayerPokemon.Client.Clients;
using MultiplayerPokemon.Client.Settings;
using MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.ConnectionActions;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.Effects;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.SignalRConnectionUseCase
{
    public class ConnectionEffectsHandler
    {
        private readonly RESTPokemonClient pokemonClient;
        public ConnectionEffectsHandler(RESTPokemonClient pokemonClient)
        {
            this.pokemonClient = pokemonClient;
        }

        [EffectMethod]
        public async Task HandleConnectionConnect(ConnectionConnectEffect action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new ConnectionBeginAction());

            try
            {
                HubConnection connection = new HubConnectionBuilder()
                    .WithUrl(AppSettings.RoomHubName)
                    .ConfigureLogging(options =>
                    {
                        options.SetMinimumLevel(LogLevel.Information);
                    })
                    .Build();

                connection.On<string>("TestRoomHubInvoke", (response) =>
                {
                    Console.WriteLine(response);
                    action.UpdateState();
                });

                connection.On<RoomModel>("GetConnectedRoomInfo", async (room) =>
                {
                    Console.WriteLine($"Connected To Room: { room.RoomName}" );
                    var roomParty = await pokemonClient.GetRoomParty(room.PokemonParty.Pokemon);
                    dispatcher.Dispatch(new ConnectedToRoomAction(room, roomParty));
                    action.UpdateState();
                });

                connection.On<PokemonPartyDataModel>("PokemonAddedToParty", async (partyData) =>
                {
                    Console.WriteLine($"Adding pokemon with id {partyData.PokedexId} to party");
                    var partyPokemonData = await pokemonClient.GetPartyCard(partyData.PokedexId.ToString(), partyData.Gender, partyData.IsShiny);
                    if (partyPokemonData is not null)
                        dispatcher.Dispatch(new AddPokemonToPartyAction(partyPokemonData));
                    action.UpdateState();
                });

                connection.On<int>("RemovePokemonFromParty", (cardID) =>
                {
                    Console.WriteLine($"Removing Pokemon with Id {cardID} from party");
                    dispatcher.Dispatch(new RemovePokemonFromPartyAction(cardID));
                    action.UpdateState();
                });

                connection.On<int, int>("PokemonSwapped", (currentPos, newPos) =>
                {
                    Console.WriteLine($"Swapping Pokemon in current position {currentPos} to {newPos}");
                    dispatcher.Dispatch(new PokemonSwappedAction(currentPos, newPos));
                    action.UpdateState();
                });

                connection.On<MessageModel>("MessageSentToRoom", (message) =>
                {
                    Console.WriteLine($"Adding message to chat: {message.MessageText}");
                    dispatcher.Dispatch(new AddMessageAction(message));
                    action.UpdateState();
                });

                connection.On<UserModel, MessageModel>("NewUserConnectedToRoom", (user, message) =>
                {
                    Console.WriteLine(message.MessageText);
                    dispatcher.Dispatch(new AddUserToRoomAction(user, message));
                    action.UpdateState();
                });

                connection.On<string, MessageModel>("UserDisconnectedFromRoom", (user, message) =>
                {
                    Console.WriteLine(message);
                    dispatcher.Dispatch(new RemoveUserFromRoomAction(user, message));
                    action.UpdateState();
                });

                connection.On("DisconnectUserFromRoom", () => {
                    Console.WriteLine("Forcibly Removing User From Current Room");
                    dispatcher.Dispatch(new DisconnectUserFromRoomAction());
                    action.UpdateState();
                });

                connection.On("DisconnectUserFromSignalR", () =>
                {
                    Console.WriteLine("Disconnecting User From SignalR Connection...");
                    dispatcher.Dispatch(new DisconnectUserFromSignalRAction());
                    action.UpdateState();
                });

                connection.On<string>("ThrowError", (error) =>
                {
                    Console.WriteLine(error);
                });

                await connection.StartAsync();

                await connection.SendAsync("TestRoomHub", action.User.Username);

                dispatcher.Dispatch(new ConnectionSuccessAction(connection));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new ConnectionDroppedAction("Count not instantiate Connection... Exception: " + ex.Message));
            }
        }
    }
}
