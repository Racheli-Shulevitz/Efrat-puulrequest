using Entities;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task<Rating> post(Rating rating);
    }
}