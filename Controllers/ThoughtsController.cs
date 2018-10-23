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
    public class ThoughtsController : ControllerBase
    {
        private readonly IThoughtRepository repo;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;

        public ThoughtsController(IThoughtRepository repo, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.hostingEnvironment = hostingEnvironment;
        }

        [Authorize]
        [HttpPost("create")]
        public ActionResult CreateThought()
        {
            var description = Request.Form["description"];
            var visible = Request.Form["visible"];
            var file = Request.Form.Files[0];

            if
            (
                (file.ContentType.ToLower() != "image/jpg" ||
                file.ContentType.ToLower() != "image/pjpeg" ||
                file.ContentType.ToLower() != "image/jpeg") &&
                (Path.GetExtension(file.FileName).ToLower() != ".jpg" ||
                Path.GetExtension(file.FileName).ToLower() != ".jpeg")
            )
            {
                var thoughtToSave = new Thought();
                thoughtToSave.Description = description;
                if (visible == "Widoczny")
                    thoughtToSave.Visible = 1;
                else thoughtToSave.Visible = 0;

                string folderName = "thoughts";
                string webRootPath = this.hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = DateTime.Now.Ticks.ToString();
                    fileName += ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    thoughtToSave.ThoughtPhotoUrl = folderName + '\\' + fileName;
                }

                this.repo.CreateThought(thoughtToSave);
                return Ok();
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThought(int id)
        {
            return Ok();
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNewsThoughts()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllThoughts()
        {
            return Ok();
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateThought()
        {
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThought(int id)
        {
            return Ok();
        }
    }
}
