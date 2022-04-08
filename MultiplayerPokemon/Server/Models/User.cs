using MultiplayerPokemon.Server.Settings;
using MultiplayerPokemon.Shared.Dtos;
using System.Security.Cryptography;

namespace MultiplayerPokemon.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        private User() { }

        public User(int id, RegisterRequest userDto)
        {
            Id = id;
            Username = userDto.Username;
            Email = userDto.Email;
            using (var hmac = new HMACSHA512(AppSettings.PasswordSalt))
            {
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
            }
            IsAdmin = true;
        }

        public bool PasswordMatch(string password)
        {
            using (var hmac = new HMACSHA512(AppSettings.PasswordSalt))
            {
                byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return PasswordHash.SequenceEqual(passwordHash);
            }
        }
    }
}
