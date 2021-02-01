using Newtonsoft.Json;
using SOA.GatewayApi.Entities;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SOA.GatewayApi.Services
{
    public interface IMusicService
    {
        public Task<SongDetails> GetSongByLyrics(string lyrics);
    }

    public class MusicService : IMusicService
    {
        private readonly ApiSettings _apiSettings;

        public MusicService(ApiSettings settings)
        {
            _apiSettings = settings;
        }

        public async Task<SongDetails> GetSongByLyrics(string lyrics)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiSettings.MusicApiUrl + "/songs?lyrics=" +lyrics);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("ApiKey", _apiSettings.MusicApiKey);

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SongDetails>(stringResult);
                return result;
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
