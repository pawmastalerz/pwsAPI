using Microsoft.AspNetCore.Hosting;
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
    }
}