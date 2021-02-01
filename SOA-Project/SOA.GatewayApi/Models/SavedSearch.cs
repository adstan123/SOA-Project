using Newtonsoft.Json;

namespace SOA.GatewayApi.Entities
{
    public class SavedSearch
    { 
        public string Username { get; set; }
        public string Lyrics { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
    }
}
