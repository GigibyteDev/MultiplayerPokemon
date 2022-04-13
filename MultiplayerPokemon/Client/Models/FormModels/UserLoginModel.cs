using System.ComponentModel.DataAnnotations;

namespace MultiplayerPokemon.Client.Models
{
    public class UserLoginModel
    {
        [Required]
        [MaxLength(128)]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [MaxLength(128)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
