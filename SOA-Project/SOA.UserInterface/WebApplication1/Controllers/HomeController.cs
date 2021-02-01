using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SOA.GatewayApi.Entities;
using SOA.GatewayApi.Models;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        private readonly string baseUrl = "https://soa-gatewayapi";
        private readonly IDistributedCache _cache;
        private readonly string tokenKey = "token";
        private readonly string usernameKey = "username";

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _client = new HttpClient(clientHandler);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxMethod(string lyrics)
        {
            return Json(new { });
        }

        [HttpPost]
        public async Task<JsonResult> LoginMethod(string username, string password)
        {
            var uri = baseUrl + "/users/authentication";
            var request = new AuthenticateRequest()
            {
                Username = username,
                Password = password
            };
            var jsonInString = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthenticateResponse>(stringResult);
                _cache.SetString(usernameKey, result.Username);
                _cache.SetString(tokenKey, result.JwtToken);
                return Json(new { valid = true });

            }
            else
            {
                return Json(new { valid = false });

            }
        }

        [HttpPost]
        public JsonResult SignOutMethod()
        {
            _cache.SetString(usernameKey, string.Empty);
            _cache.SetString(tokenKey, string.Empty);
            return Json(new { valid = true });
        }

        [HttpPost]
        public async Task<JsonResult> GetSongMethod(string lyrics)
        {
            var uri = baseUrl + "/songs?lyrics=" + lyrics;
            var request = new HttpRequestMessage(HttpMethod.Get,uri);
            AddHeaders(request);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SongDetails>(stringResult);
                return Json(result);

            }
            else
            {
                return Json(new { valid = false });
            }        
        }

        [HttpGet]
        public async Task<JsonResult> GetSongsMethod()
        {
            var uri = baseUrl + "/savedSearches";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            AddHeaders(request);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<SavedSearch>>(stringResult);
                return Json(result);

            }
            else
            {
                return Json(new { valid = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveSearchMethod(string lyrics,string artist,string song)
        {
            var uri = baseUrl + "/savedSearches";
            var username = _cache.GetString(usernameKey);
            var token = _cache.GetString(tokenKey);
            var request = new SavedSearch()
            {
                Lyrics=lyrics, 
                Artist=artist,
                Song=song,
                Username=username 
            };
            var jsonInString = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { valid = true });

            }
            else
            {
                return Json(new { valid = false });

            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void AddHeaders(HttpRequestMessage request)
        {
            var token = _cache.GetString(tokenKey);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer " + token);
        }
    }
}
