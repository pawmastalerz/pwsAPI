using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pwsAPI.Data;
using pwsAPI.Models;

namespace pwsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostersController : ControllerBase
    {
        private readonly IPosterRepository repo;
        private readonly IMapper mapper;

        public PostersController(IPosterRepository repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoster(int id)
        {
            var posterToReturn = await this.repo.GetPoster(id);
            return Ok(posterToReturn);
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNewsPosters()
        {
            var postersToReturn = await this.repo.GetNewsPosters();

            if (postersToReturn != null)
                return Ok(postersToReturn);

            throw new Exception($"Błąd pobierania plakatów do aktualności");
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPosters()
        {
            var postersToReturn = await this.repo.GetAllPosters();

            if (postersToReturn != null)
                return Ok(postersToReturn);

            throw new Exception($"Błąd pobierania listy plakatów");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePoster(Poster poster)
        {
            var posterInRepo = await this.repo.GetPoster(poster.Id);
            mapper.Map(poster, posterInRepo);

            if (await this.repo.SaveAll())
                return Ok();

            throw new Exception($"Błąd aktualizacji plakatu o id: {poster.Id}");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var posterInRepo = await this.repo.GetPoster(id);
            this.repo.Delete(posterInRepo);

            if (await this.repo.SaveAll())
                return NoContent();

            throw new Exception($"Błąd usuwania plakatu o id: {id}");
        }
    }
}
