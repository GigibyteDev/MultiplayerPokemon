namespace MultiplayerPokemon.Client.Store.RoomUseCase.RoomActions
{
    public class UpdateSearchedPokemonGenderAction
    {
        public string Gender { get; set; }

        public UpdateSearchedPokemonGenderAction(string gender)
        {
            Gender = gender;
        }
    }
}
