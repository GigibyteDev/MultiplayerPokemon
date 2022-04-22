namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class RemoveMultiplePokemonFromPartyAction
    {
        public IEnumerable<int> Positions { get; set; }

        public RemoveMultiplePokemonFromPartyAction(IEnumerable<int> positions)
        {
            Positions = positions;
        }
    }
}
