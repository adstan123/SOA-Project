using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOA.GatewayApi.Attributes;
using SOA.GatewayApi.Entities;
using SOA.GatewayApi.Services;

namespace SOA.GatewayApi.Controllers
{
    [ValidateToken]
    [ApiController]
    [Route("[controller]")]
    public class SavedSearchesController : ControllerBase
    {
        private readonly ISavedSearchService _service;

        public SavedSearchesController(ISavedSearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetByUsername()
        {
            var username = HttpContext.TraceIdentifier;
            var results = await _service.GetSavedSearchesByUsername(username);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SavedSearch request)
        {
            request.Username = HttpContext.TraceIdentifier;
            var results = await _service.SaveSearch(request);
            return Ok(results);
        }
    }
}
