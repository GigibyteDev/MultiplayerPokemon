namespace MultiplayerPokemon.Shared.Dtos
{
    public class RegisterResult
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
