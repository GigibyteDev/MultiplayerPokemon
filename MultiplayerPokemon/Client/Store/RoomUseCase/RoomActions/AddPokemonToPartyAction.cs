using MultiplayerPokemon.Client.Models;
using MultiplayerPokemon.Shared.Models;

namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class AddPokemonToPartyAction
    {
        public PartyCardModel PartyCardModel { get; set; }

        public AddPokemonToPartyAction(PartyCardModel partyCardModel)
        {
            PartyCardModel = partyCardModel;
        }
    }
}
