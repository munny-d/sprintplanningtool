namespace sprintplanningtoolbackend.Authorisation
{
    using sprintplanningtoolbackend.Models;

    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public int? ValidateToken(string token);
    }
}
