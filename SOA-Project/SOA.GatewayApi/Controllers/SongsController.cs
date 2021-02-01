using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOA.GatewayApi.Attributes;
using SOA.GatewayApi.Services;

namespace SOA.MusicApi.Controllers
{
    [ValidateToken]
    [ApiController]
    [Route("[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly IMusicService _service;

        public SongsController(IMusicService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSongByLyrics([FromQuery]string lyrics)
        {
            var results = await _service.GetSongByLyrics(lyrics);
            return Ok(results);
        }
    }
}
