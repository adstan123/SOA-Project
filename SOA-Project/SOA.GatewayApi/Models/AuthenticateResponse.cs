namespace SOA.GatewayApi.Models
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
    }
}