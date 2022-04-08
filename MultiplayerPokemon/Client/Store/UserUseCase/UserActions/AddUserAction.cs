using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.UserUseCase.UserActions
{
    public class AddUserAction
    {
        public UserModel User { get; set; }
        public AddUserAction(UserModel user)
        {
            User = user;
        }
    }
}
