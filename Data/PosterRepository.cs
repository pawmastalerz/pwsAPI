using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public class PosterRepository : IPosterRepository
    {
        private readonly DataContext context;

        public PosterRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Poster>> GetNewsPosters()
        {
            return await this.context.Posters
                .FromSql("SELECT * FROM pws.Posters WHERE HappensAt > now() ORDER BY HappensAt LIMIT 3")
                .ToListAsync();
        }

        public async Task<List<Poster>> GetAllPosters()
        {
            return await this.context.Posters
                .FromSql("SELECT * FROM pws.Posters ORDER BY HappensAt DESC")
                .ToListAsync();
        }
    }
}