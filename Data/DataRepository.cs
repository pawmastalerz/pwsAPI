using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pwsAPI.Models;

namespace pwsAPI.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext context;

        public DataRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Event> GetLatestEvent()
        {
            return await this.context.Events.FirstOrDefaultAsync();
        }
    }
}