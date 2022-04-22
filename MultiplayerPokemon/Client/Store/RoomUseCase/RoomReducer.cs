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
            if (state.SelectedCards.Contains(action.CardId))
            {
                state.SelectedCards.Remove(action.CardId);
            }
            
            for (int i = 0; i < state.SelectedCards.Count(); i++)
            {
                if (state.SelectedCards[i] > action.CardId)
                {
                    state.SelectedCards[i]--;
                }
            }

            return new RoomState
            (
                previousState: state
            );
        }

        [ReducerMethod]
        public static RoomState PokemonSwappedAction(RoomState state, PokemonSwappedAction action)
        {
            state.PokemonParty.Cards.Swap(action.CurrentPosition, action.NewPosition);
            if (!state.SelectedCards.Contains(action.CurrentPosition) || !state.SelectedCards.Contains(action.NewPosition))
            {
                if (state.SelectedCards.Contains(action.CurrentPosition))
                {
                    state.SelectedCards.Remove(action.CurrentPosition);
                    state.SelectedCards.Add(action.NewPosition);
                }
                else if (state.SelectedCards.Contains(action.NewPosition))
                {
                    state.SelectedCards.Remove(action.NewPosition);
                    state.SelectedCards.Add(action.CurrentPosition);
                }
            }
           
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

        [ReducerMethod]
        public static RoomState SelectCardAction(RoomState state, SelectCardAction action)
        {
            state.SelectedCards.Add(action.CardId);

            return new RoomState
            (
                previousState: state
            );
        }

        [ReducerMethod]
        public static RoomState DeselectCardAction(RoomState state, DeselectCardAction action)
        {
            state.SelectedCards.Remove(action.CardId);

            return new RoomState
            (
                previousState: state
            );
        }
    }
}
