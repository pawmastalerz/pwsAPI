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
    public class PostersController : ControllerBase
    {
        private readonly IDataRepository repo;

        public PostersController(IDataRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var postersToReturn = await this.repo.GetNewsPosters();
            return Ok(postersToReturn);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var postersToReturn = await this.repo.GetAllPosters();
            return Ok(postersToReturn);
        }

        // GET api/posters/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/posters
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/posters/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/posters/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
