using System.Linq;

namespace SOA.MusicApi.Entities
{
    public class SongDetails
    {
        public SongDetails()
        {
        }

        public SongDetails(SongResponse response,string lyrics)
        {
            Lyrics = lyrics;
            Artist = response?.message?.body?.track_list?.FirstOrDefault()?.track?.artist_name;
            Song = response?.message?.body?.track_list?.FirstOrDefault()?.track?.track_name;
        }

        public string Artist { get; set; }
        public string Song { get; set; }
        public string Lyrics { get; set; }
    }
}
