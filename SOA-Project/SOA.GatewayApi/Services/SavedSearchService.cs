using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SOA.GatewayApi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SOA.GatewayApi.Services
{
    public interface ISavedSearchService
    {
        public Task<SavedSearch> SaveSearch(SavedSearch search);
        public Task<List<SavedSearch>> GetSavedSearchesByUsername(string username);
    }

    public class SavedSearchService : ISavedSearchService
    {
        private readonly ApiSettings _apiSettings;
        public SavedSearchService(ApiSettings settings)
        {
            _apiSettings = settings;
        }

        public async Task<List<SavedSearch>> GetSavedSearchesByUsername(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiSettings.DataApiUrl + "/savedSearches/" + username);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("ApiKey", _apiSettings.DataApiKey);


            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<SavedSearch>>(stringResult);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<SavedSearch> SaveSearch(SavedSearch search)
        {
            var uri = _apiSettings.DataApiUrl + "/savedSearches";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var jsonInString = JsonConvert.SerializeObject(search);
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("ApiKey", _apiSettings.DataApiKey);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SavedSearch>(stringResult);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
