namespace MultiplayerPokemon.Server.Settings
{
    public static class AppSettings
    {
        public static byte[] PasswordSalt { get; set; } = new byte[0];
        public static string JwtSecret { get; set; } = string.Empty;
        public static string Issuer { get; set; } = string.Empty;
        public static string Audience { get; set; } = string.Empty;
    }
}
