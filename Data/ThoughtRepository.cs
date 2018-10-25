using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public class ThoughtRepository : IThoughtRepository
    {
        private readonly DataContext context;
        private readonly IHostingEnvironment hostingEnvironment;

        public ThoughtRepository(DataContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }
        public bool CreateThought(Thought thought)
        {
            this.context.Thoughts.Add(thought);
            this.context.SaveChanges();
            return true;
        }

        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public void DeleteFile(Thought thought)
        {
            var physicalPath = this.hostingEnvironment.WebRootPath + '\\' + thought.ThoughtPhotoUrl;
            File.Delete(physicalPath);
        }

        public async Task<List<Thought>> GetAllThoughts()
        {
            return await this.context.Thoughts
                .FromSql("SELECT * FROM pws.Thoughts")
                .ToListAsync();
        }

        public async Task<List<Thought>> GetNewsThoughts()
        {
            return await this.context.Thoughts
                .FromSql("SELECT * FROM pws.Thoughts WHERE Accepted = 1")
                .ToListAsync();
        }

        public async Task<Thought> GetThought(int id)
        {
            var thoughtToReturn = await this.context.Thoughts
                .FirstOrDefaultAsync(p => p.Id == id);
            return thoughtToReturn;
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}