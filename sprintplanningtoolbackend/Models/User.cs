namespace sprintplanningtoolbackend.Models
{
    using System.Text.Json.Serialization;
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public bool? IsAdmin { get; set; }

        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
