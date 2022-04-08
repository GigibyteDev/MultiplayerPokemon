using Fluxor;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.UserUseCase
{
    [FeatureState]
    public class UserState
    {
        public UserModel User { get; set; }

        public UserState()
        {
            User = new UserModel()
            {
                Id = -1,
                Username = "Anonymous",
                IsAdmin = false,
            };
        }
        public UserState(UserState previousState, UserModel? user = null)
        {
            User = user ?? previousState.User;
        }
    }
}
