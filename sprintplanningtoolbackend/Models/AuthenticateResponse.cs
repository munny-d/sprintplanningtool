namespace sprintplanningtoolbackend.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public bool IsAdmin { get; set; }  
        public string? Token { get; set; }
    }
}