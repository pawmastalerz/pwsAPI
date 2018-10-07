using System.Collections.Generic;
using System.Threading.Tasks;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public interface IPosterRepository
    {
        Task<Poster> GetPoster(int id);
        Task<List<Poster>> GetNewsPosters();
        Task<List<Poster>> GetAllPosters();
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
    }
}