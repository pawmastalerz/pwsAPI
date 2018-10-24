using System.Collections.Generic;
using System.Threading.Tasks;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public interface IThoughtRepository
    {
        bool CreateThought(Thought thought);
        Task<Thought> GetThought(int id);
        Task<List<Thought>> GetNewsThoughts();
        Task<List<Thought>> GetAllThoughts();
        void Delete<T>(T entity) where T : class;
        void DeleteFile(Thought thought);
        Task<bool> SaveAll();
    }
}