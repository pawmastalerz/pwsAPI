using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pwsAPI.Data;

namespace pwsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IDataRepository repo;

        public EventsController(IDataRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            var eventToReturn = await this.repo.GetLatestEvent();
            return Ok(eventToReturn);
        }
    }
}
