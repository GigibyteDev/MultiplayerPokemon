using System.ComponentModel.DataAnnotations;

namespace MultiplayerPokemon.Client.Models
{
    public class RegisterUserModel
    {
        [Required]
        [MaxLength(128)]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(128)]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [MaxLength(128)]
        [MinLength(4)]
        [Display(Name = "Password Confirmation")]
        public string PasswordCheck { get; set; }
    }
}
