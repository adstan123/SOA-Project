using System.Text.Json.Serialization;

namespace SOA.DataApi.Entities
{
    public class SavedSearch
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Lyrics { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
    }
}
