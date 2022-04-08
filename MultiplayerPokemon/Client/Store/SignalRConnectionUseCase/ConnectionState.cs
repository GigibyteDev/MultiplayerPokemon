using Fluxor;
using Microsoft.AspNetCore.SignalR.Client;

namespace MultiplayerPokemon.Client.Store.SignalRConnectionUseCase
{
    [FeatureState]
    public class ConnectionState
    {
        public HubConnection? Connection { get; set; }
        public bool Loading { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }

        public ConnectionState()
        {
            Connection = null;
            Loading = false;
            Response = string.Empty;
            Error = string.Empty;
        }

        public ConnectionState(ConnectionState previousConnection, HubConnection? connection = null, bool? loading = null, string? response = null, string? error = null)
        {
            Connection = connection ?? previousConnection.Connection;
            Loading = loading ?? previousConnection.Loading;
            Response = response ?? previousConnection.Response;
            Error = error ?? previousConnection.Error;
        }
    }
}
