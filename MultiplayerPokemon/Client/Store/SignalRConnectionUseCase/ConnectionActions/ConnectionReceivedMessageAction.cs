namespace MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.ConnectionActions
{
    public class ConnectionReceivedMessageAction
    {
        public string Message { get; set; }

        public ConnectionReceivedMessageAction(string message)
        {
            Message = message;
        }
    }
}
