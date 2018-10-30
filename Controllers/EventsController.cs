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
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository repo;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;

        public EventsController(IEventRepository repo, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.hostingEnvironment = hostingEnvironment;
        }

        [Authorize]
        [HttpPost("create")]
        public ActionResult CreateEvent()
        {
            var eventName = Request.Form["eventName"];
            var happensAt = Request.Form["happensAt"];
            var signUpLink = Request.Form["signUpLink"];
            var accepted = Request.Form["accepted"];

            var eventToSave = new Event();
            eventToSave.EventName = eventName;
            eventToSave.HappensAt = DateTime.Parse(happensAt);
            eventToSave.SignUpLink = signUpLink;
            if (accepted == "Przyjęto")
                eventToSave.Accepted = 1;
            else eventToSave.Accepted = 0;

            if (Request.Form.Files.Count > 0)
            {
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
                    string folderName = "events";
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
                        eventToSave.PosterPhotoUrl = folderName + '\\' + fileName;
                    }
                }
            }
            else
            {
                return BadRequest();
            }

            this.repo.CreateEvent(eventToSave);
            return Ok();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventToReturn = await this.repo.GetEvent(id);
            return Ok(eventToReturn);
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNewsEvents()
        {
            var eventsToReturn = await this.repo.GetNewsEventsPosters();

            if (eventsToReturn != null)
                return Ok(eventsToReturn);

            return BadRequest();
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllEvents()
        {
            var eventsToReturn = await this.repo.GetAllEvents();

            if (eventsToReturn != null)
                return Ok(eventsToReturn);

            return BadRequest();
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEvent()
        {
            var eventId = Request.Form["eventId"];
            var eventName = Request.Form["eventName"];
            var happensAt = Request.Form["happensAt"];
            var signUpLink = Request.Form["signUpLink"];
            var accepted = Request.Form["accepted"];

            Event eventInRepo = await this.repo.GetEvent(Convert.ToInt16(eventId));
            EventForUpdateDto eventForUpdateDto = new EventForUpdateDto();
            eventForUpdateDto.EventId = Convert.ToInt16(eventId);
            eventForUpdateDto.EventName = eventName;
            eventForUpdateDto.HappensAt = Convert.ToDateTime(happensAt);
            if (accepted == "Przyjęto")
                eventForUpdateDto.Accepted = 1;
            else eventForUpdateDto.Accepted = 0;
            eventForUpdateDto.SignUpLink = signUpLink;

            if (Request.Form.Files.Count > 0)
            {
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
                    string folderName = "events";
                    string webRootPath = this.hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        return BadRequest();
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
                        this.repo.DeletePosterFile(eventInRepo);
                        eventForUpdateDto.PosterPhotoUrl = folderName + '\\' + fileName;
                    }
                }
            }
            else
            {
                eventForUpdateDto.PosterPhotoUrl = eventInRepo.PosterPhotoUrl;
            }

            mapper.Map(eventForUpdateDto, eventInRepo);

            if (await this.repo.SaveAll())
                return Ok();

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventInRepo = await this.repo.GetEvent(id);
            this.repo.DeletePosterFile(eventInRepo);
            this.repo.Delete(eventInRepo);

            if (await this.repo.SaveAll())
                return Ok();

            return NoContent();
        }
    }
}
