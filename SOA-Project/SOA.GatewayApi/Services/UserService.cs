using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SOA.GatewayApi.Models;

namespace SOA.GatewayApi.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<string> GetUserFromToken(string token);
    }

    public class UserService : IUserService
    {
        private readonly ApiSettings _apiSettings;

        public UserService(
            ApiSettings settings)
        {
            _apiSettings = settings;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var uri=_apiSettings.SecurityApiUrl + "/users/authentication";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var jsonInString = JsonConvert.SerializeObject(model);
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("ApiKey", _apiSettings.SecurityApiKey);
            var content= new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri,content);


            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthenticateResponse>(stringResult);
                return result;
            }
            else
            {
                return null; 
            }
        }

      
        public async Task<string> GetUserFromToken(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiSettings.SecurityApiUrl + "/users/validation");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("ApiKey", _apiSettings.SecurityApiKey);
            request.Headers.Add("Authorization","Bearer "+token);


            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                result= result.Remove(0,1);
                result = result.Remove(result.Length - 1,1);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}