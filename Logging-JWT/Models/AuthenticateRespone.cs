using Logging_JWT.Entities;

namespace Logging_JWT.Models
{
    public class AuthenticateRespone
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }

        public AuthenticateRespone(User user, string? token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Token = token;
        }
    }
}
