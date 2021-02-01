using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOA.Common;
using SOA.MusicApi.Services;

namespace SOA.MusicApi.Controllers
{
    [ApiKey]
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
