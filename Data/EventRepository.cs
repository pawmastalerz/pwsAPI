using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext context;
        private readonly IHostingEnvironment hostingEnvironment;

        public EventRepository(DataContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public bool CreateEvent(Event ev)
        {
            this.context.Events.Add(ev);
            this.context.SaveChanges();
            return true;
        }

        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public void DeletePosterFile(Event ev)
        {
            var physicalPath = this.hostingEnvironment.WebRootPath + '\\' + ev.PosterPhotoUrl;
            File.Delete(physicalPath);
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await this.context.Events
                .FromSql("SELECT * FROM pws.Events ORDER BY HappensAt DESC")
                .ToListAsync();
        }

        public async Task<Event> GetEvent(int id)
        {
            var eventToReturn = await this.context.Events
                .FirstOrDefaultAsync(p => p.EventId == id);
            return eventToReturn;
        }

        public async Task<List<Event>> GetNewsEventsPosters()
        {
            return await this.context.Events
                .FromSql("SELECT * FROM pws.Events WHERE HappensAt > now() AND Accepted = 1 ORDER BY HappensAt LIMIT 3")
                .ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}