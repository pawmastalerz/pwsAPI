using pwsAPI.Models;

namespace pwsAPI.Data
{
    public interface IThoughtRepository
    {
         bool CreateThought(Thought thought);
    }
}