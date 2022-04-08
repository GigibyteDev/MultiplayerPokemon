using MultiplayerPokemon.Server.Settings;

namespace MultiplayerPokemon.Server.Extensions
{
    public static class AppSettingsExtentions
    {
        public static void InitializeAppSettings(this ConfigurationManager config)
        {
            string secret = config.GetSection("AppSettings:PasswordEncryptionSecret").Value;

            AppSettings.PasswordSalt = Convert.FromBase64String(secret);
            AppSettings.JwtSecret = config.GetSection("AppSettings:JwtSecret").Value;
            AppSettings.Issuer = config.GetSection("AppSettings:Issuer").Value;
            AppSettings.Audience = config.GetSection("AppSettings:Audience").Value;
        }
    }
}
