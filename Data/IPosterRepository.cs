using System.Collections.Generic;
using System.Threading.Tasks;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public interface IPosterRepository
    {
        bool CreatePoster(Poster poster);
        Task<Poster> GetPoster(int id);
        Task<List<Poster>> GetNewsPosters();
        Task<List<Poster>> GetAllPosters();
        void Delete<T>(T entity) where T : class;
        void DeleteFile(Poster poster);
        Task<bool> SaveAll();
    }
}