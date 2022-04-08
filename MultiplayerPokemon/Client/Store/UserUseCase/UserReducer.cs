using Fluxor;
using MultiplayerPokemon.Client.Store.UserUseCase.UserActions;

namespace MultiplayerPokemon.Client.Store.UserUseCase
{
    public static class UserReducer
    {
        [ReducerMethod]
        public static UserState AddUserAction(UserState state, AddUserAction action) =>
            new(
                    previousState: state,
                    user: action.User
                );
    }
}
