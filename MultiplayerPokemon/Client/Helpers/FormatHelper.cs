using System.Globalization;

namespace MultiplayerPokemon.Client.Helpers
{
    public static class FormatHelper
    {
        private static readonly TextInfo ti = new CultureInfo("en-US", false).TextInfo;

        public static string ToDisplayName(this string input)
        {
            return ti.ToTitleCase(input.Replace('-', ' ').Trim());
        }

        public static string FromDisplayName(this string input)
        {
            return input.Trim().Replace(' ', '-').ToLower();
        }
    }
}
