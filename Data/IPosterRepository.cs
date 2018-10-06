using System.Collections.Generic;
using System.Threading.Tasks;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public interface IPosterRepository
    {
        Task<List<Poster>> GetNewsPosters();
        Task<List<Poster>> GetAllPosters();
    }
}