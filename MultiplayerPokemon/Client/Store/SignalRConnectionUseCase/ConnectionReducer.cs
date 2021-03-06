using Fluxor;
using MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.ConnectionActions;

namespace MultiplayerPokemon.Client.Store.SignalRConnectionUseCase
{
    public static class ConnectionReducer
    {
        [ReducerMethod(typeof(ConnectionBeginAction))]
        public static ConnectionState ConnectionBeginAction(ConnectionState state) =>
            new (
                    previousConnection: state,
                    loading: true
                );

        [ReducerMethod]
        public static ConnectionState ConnectionSuccessAction(ConnectionState state, ConnectionSuccessAction action) =>
            new (
                    previousConnection: state,
                    loading: false,
                    error: string.Empty,
                    connection: action.Connection
                );

        [ReducerMethod]
        public static ConnectionState ConnectionDroppedAction(ConnectionState state, ConnectionDroppedAction action) =>
            new (
                    previousConnection: state,
                    loading: false,
                    error: action.Error
                );

        [ReducerMethod]
        public static ConnectionState ConnectionReceivedMessageAction(ConnectionState state, ConnectionReceivedMessageAction action) =>
            new (
                    previousConnection: state,
                    response: action.Message
                );

        [ReducerMethod(typeof(CloseConnectionAction))]
        public static ConnectionState CloseConnectionAction(ConnectionState state)
        {
            if (state.Connection is not null)
            {
                Task.Run(() => state.Connection.DisposeAsync());
            }
            return new ConnectionState();
        }

        [ReducerMethod(typeof(DisconnectUserFromSignalRAction))]
        public static ConnectionState DisconnectUserFromSignalRAction(ConnectionState state)
        {
            if (state.Connection is not null)
            {
                Task.Run(() => state.Connection.DisposeAsync());
            }
            return new ConnectionState();
        }
    }
}
