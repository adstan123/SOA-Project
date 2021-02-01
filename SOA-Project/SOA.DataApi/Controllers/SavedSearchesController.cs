using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOA.Common;
using SOA.DataApi.Entities;
using SOA.DataApi.Services;

namespace SOA.DataApi.Controllers
{
    [ApiKey]
    [ApiController]
    [Route("[controller]")]
    public class SavedSearchesController : ControllerBase
    {
        private readonly ISavedSearchService _service;

        public SavedSearchesController(ISavedSearchService service)
        {
            _service = service;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var results = await _service.GetSavedSearchesByUsername(username);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SavedSearch request)
        {
            var results = await _service.SaveSearch(request);
            return Ok(results);
        }
    }
}
