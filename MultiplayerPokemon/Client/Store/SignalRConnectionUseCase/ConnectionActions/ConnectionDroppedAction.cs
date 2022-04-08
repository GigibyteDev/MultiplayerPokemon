namespace MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.ConnectionActions
{
    public class ConnectionDroppedAction
    {
        public string Error { get; set; }

        public ConnectionDroppedAction(string error)
        {
            Error = error;
        }
    }
}
