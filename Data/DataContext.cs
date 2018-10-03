using Microsoft.EntityFrameworkCore;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Poster> Posters { get; set; }
    }
}