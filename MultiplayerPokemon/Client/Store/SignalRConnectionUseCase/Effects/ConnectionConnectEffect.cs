using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.SignalRConnectionUseCase.Effects
{
    public class ConnectionConnectEffect
    {
        public UserModel User { get; set; }
        public Action UpdateState { get; set; }
        public ConnectionConnectEffect(UserModel user, Action updateState)
        {
            User = user;
            UpdateState = updateState;
        }
    }
}
