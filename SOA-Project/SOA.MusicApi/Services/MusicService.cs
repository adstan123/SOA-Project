using SOA.Common;
using SOA.MusicApi.Entities;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SOA.MusicApi.Services
{
    public interface IMusicService
    {
        public Task<SongDetails> GetSongByLyrics(string lyrics);
    }

    public class MusicService : IMusicService
    {
        private readonly string ApiKey = "ae59d87d6c26c922d8083f8737285258";
        private readonly IHttpClientFactory _clientFactory;

        public MusicService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<SongDetails> GetSongByLyrics(string lyrics)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://api.musixmatch.com/ws/1.1/track.search?format=json&q_lyrics=" + lyrics + "&quorum_factor=1&apikey=" + ApiKey);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync
                    <SongResponse>(responseStream);
                return new SongDetails(result, lyrics);
            }
            else
            {
                return new SongDetails()
                {
                    Artist = "Unknown",
                    Song = "Unknown",
                    Lyrics = lyrics
                };
            }
        }
    }
}
