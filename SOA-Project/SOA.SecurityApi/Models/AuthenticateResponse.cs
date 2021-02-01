using SOA.SecurityApi.Entities;

namespace SOA.SecurityApi.Models
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }


        public AuthenticateResponse(User user, string jwtToken)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            JwtToken = jwtToken;
        }
    }
}