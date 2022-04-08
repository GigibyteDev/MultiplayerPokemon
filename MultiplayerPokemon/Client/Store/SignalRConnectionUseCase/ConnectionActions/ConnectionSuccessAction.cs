using Microsoft.AspNetCore.SignalR.Client;

namespace MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.ConnectionActions
{
    public class ConnectionSuccessAction
    {
        public HubConnection Connection { get; set; }

        public ConnectionSuccessAction(HubConnection connection)
        {
            Connection = connection;
        }
    }
}
