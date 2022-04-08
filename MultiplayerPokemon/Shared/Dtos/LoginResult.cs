namespace MultiplayerPokemon.Shared.Dtos
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string JWT { get; set; }
        public string ErrorMessage { get; set; }
    }
}
