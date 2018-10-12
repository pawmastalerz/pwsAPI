using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using pwsAPI.Data;
using pwsAPI.Dtos;
using pwsAPI.Models;

namespace pwsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostersController : ControllerBase
    {
        private readonly IPosterRepository repo;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;

        public PostersController(IPosterRepository repo, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.hostingEnvironment = hostingEnvironment;
        }

        [Authorize]
        [HttpPost("create")]
        public ActionResult CreatePoster()
        {
            var description = Request.Form["description"];
            var happensAt = Request.Form["happensAt"];
            var visible = Request.Form["visible"];
            var file = Request.Form.Files[0];

            var posterToSave = new Poster();
            posterToSave.Description = description;
            posterToSave.HappensAt = DateTime.Parse(happensAt);
            if (visible == "true")
                posterToSave.Visible = 1;
            else posterToSave.Visible = 0;

            string folderName = "posters";
            string webRootPath = this.hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                posterToSave.PosterPhotoUrl = "http://localhost:5000/posters/" + fileName;
            }

            this.repo.CreatePoster(posterToSave);

            return Ok();
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
        public async Task<IActionResult> UpdatePoster([FromBody]PosterForUpdateDto posterForUpdateDto)
        {
            Poster posterInRepo = await this.repo.GetPoster(posterForUpdateDto.Id);

            mapper.Map(posterForUpdateDto, posterInRepo);

            if (await this.repo.SaveAll())
                return Ok();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var posterInRepo = await this.repo.GetPoster(id);
            this.repo.Delete(posterInRepo);

            if (await this.repo.SaveAll())
                return Ok();

            return NoContent();
        }
    }
}
