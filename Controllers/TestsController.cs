using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pwsAPI.Data;

namespace pwsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IDataRepository repo;

        public TestsController(IDataRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("Test passed");
        }

    }
}
