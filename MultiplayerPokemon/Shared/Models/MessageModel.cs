namespace MultiplayerPokemon.Shared.Models
{
    public class MessageModel
    {
        public UserModel User { get; set; }
        public string MessageText { get; set; }
        public DateTime SentDate { get; set; }
    }
}
