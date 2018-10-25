using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public class PosterRepository : IPosterRepository
    {
        private readonly DataContext context;
        private readonly IHostingEnvironment hostingEnvironment;

        public PosterRepository(DataContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public bool CreatePoster(Poster poster)
        {
            this.context.Posters.Add(poster);
            this.context.SaveChanges();
            return true;
        }

        public async Task<Poster> GetPoster(int id)
        {
            var posterToReturn = await this.context.Posters
                .FirstOrDefaultAsync(p => p.Id == id);
            return posterToReturn;
        }

        public async Task<List<Poster>> GetNewsPosters()
        {
            return await this.context.Posters
                .FromSql("SELECT * FROM pws.Posters WHERE HappensAt > now() AND Accepted = 1 ORDER BY HappensAt LIMIT 3")
                .ToListAsync();
        }

        public async Task<List<Poster>> GetAllPosters()
        {
            return await this.context.Posters
                .FromSql("SELECT * FROM pws.Posters ORDER BY HappensAt DESC")
                .ToListAsync();
        }

        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public void DeleteFile(Poster poster)
        {
            var physicalPath = this.hostingEnvironment.WebRootPath + '\\' + poster.PosterPhotoUrl;
            File.Delete(physicalPath);
        }

        public async Task<bool> SaveAll()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

    }
}