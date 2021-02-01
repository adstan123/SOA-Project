using System.Collections.Generic;

namespace SOA.MusicApi.Entities
{
    public class SongResponse
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public Body body { get; set; }
    }

    public class Body
    {
        public List<TrackList> track_list { get; set; }
    }

    public class TrackList
    {
        public Track track { get; set; }
    }

    public class Track
    {
        public string artist_name { get; set; }
        public string track_name { get; set; }
    }
}
