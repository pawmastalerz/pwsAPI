using System.Collections.Generic;
using System.Threading.Tasks;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public interface IEventRepository
    {
        bool CreateEvent(Event ev);
        Task<Event> GetEvent(int id);
        Task<List<Event>> GetNewsEventsPosters();
        Task<List<Event>> GetAllEvents();
        void Delete<T>(T entity) where T : class;
        void DeletePosterFile(Event ev);
        Task<bool> SaveAll();
    }
}