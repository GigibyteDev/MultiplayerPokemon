using Fluxor;
using MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions;
using MultiplayerPokemon.Shared.Logic;
using MultiplayerPokemon.Shared.Enums;

namespace MultiplayerPokemon.Client.Store.RoomUseCase
{
    public static class RoomReducer
    {
        [ReducerMethod]
        public static RoomState ConnectedToRoomAction(RoomState state, ConnectedToRoomAction action)
        {
            return new RoomState(action.RoomModel, action.PartyModel);
        }

        [ReducerMethod]
        public static RoomState AddMessageAction(RoomState state, AddMessageAction action)
        {
            var previousChat = state.Chat;

            previousChat.Messages.Add(action.Message);
            return new RoomState
                (
                    previousState: state,
                    chat: previousChat
                );
        }

        [ReducerMethod]
        public static RoomState AddUserToRoomAction(RoomState state, AddUserToRoomAction action)
        {
            var previousChat = state.Chat;
            var previousUsers = state.CurrentUsers;

            previousChat.Messages.Add(action.ConnectionMessage);
            previousUsers.Add(action.User);
            return new RoomState
                (
                    previousState: state,
                    chat: previousChat,
                    currentUsers: previousUsers
                );
        }

        [ReducerMethod]
        public static RoomState RemoveUserFromRoomAction(RoomState state, RemoveUserFromRoomAction action)
        {
            var previousChat = state.Chat;
            var previousUsers = state.CurrentUsers;

            previousChat.Messages.Add(action.DisconnectMessage);
            previousUsers.RemoveAll(u => u.Username == action.Username);

            return new RoomState
                (
                    previousState: state,
                    chat: previousChat,
                    currentUsers: previousUsers
                );
        }

        [ReducerMethod(typeof(DisconnectUserFromRoomAction))]
        public static RoomState DisconnectUserFromRoomAction(RoomState state)
        {
            return new RoomState();
        }

        [ReducerMethod]
        public static RoomState AddPokemonToPartyAction(RoomState state, AddPokemonToPartyAction action)
        {
            state.PokemonParty.Cards.AddToCollection(action.PartyCardModel);
            return new RoomState
            (
                previousState: state
            );
        }

        [ReducerMethod]
        public static RoomState RemovePokemonFromPartyAction(RoomState state, RemovePokemonFromPartyAction action)
        {
            state.PokemonParty.Cards.RemoveFromCollection(action.CardId);
            return new RoomState
            (
                previousState: state
            );
        }

        [ReducerMethod]
        public static RoomState PokemonSwappedAction(RoomState state, PokemonSwappedAction action)
        {
            state.PokemonParty.Cards.Swap(action.CurrentPosition, action.NewPosition);
            return new RoomState
            (
                previousState: state
            );
        }

        [ReducerMethod(typeof(GetPokemonStarterAction))]
        public static RoomState GetPokemonStarterAction(RoomState state)
        {
            return new RoomState
            (
                previousState: state,
                isLoadingSearchedPokemon: true
            );
        }

        [ReducerMethod(typeof(GetPokemonErrorAction))]
        public static RoomState GetPokemonErrorAction(RoomState state)
        {
            return new RoomState
            (
                previousState: state,
                isLoadingSearchedPokemon: false,
                errorGettingSearchedPokemon: true
            );
        }

        [ReducerMethod]
        public static RoomState GetPokemonSuccessAction(RoomState state, GetPokemonSuccessAction action)
        {
            var gender = state.SearchedPokemonGender;
            switch (action.PokemonModel.GenderType)
            {
                case PokemonGenderTypes.MaleOrFemale:
                    if (gender != "male" && gender != "female")
                    {
                        gender = "male";
                    }
                    break;
                case PokemonGenderTypes.MaleOnly:
                    gender = "male";
                    break;
                case PokemonGenderTypes.FemaleOnly:
                    gender = "female";
                    break;
                case PokemonGenderTypes.Genderless:
                    gender = "genderless";
                    break;
            }
            return new RoomState
            (
                previousState: state,
                isLoadingSearchedPokemon: false,
                errorGettingSearchedPokemon: false,
                searchedPokemon: action.PokemonModel,
                searchedPokemonGender: gender
            );
        }

        [ReducerMethod]
        public static RoomState UpdateSearchedPokemonGenderAction(RoomState state, UpdateSearchedPokemonGenderAction action)
        {
            return new RoomState
            (
                previousState: state,
                searchedPokemonGender: action.Gender
            );
        }

        [ReducerMethod]
        public static RoomState UpdateSearchedPokemonShinyAction(RoomState state, UpdateSearchedPokemonShinyAction action)
        {
            return new RoomState
            (
                previousState: state,
                searchedPokemonShiny: action.IsShiny
            );
        }
    }
}
