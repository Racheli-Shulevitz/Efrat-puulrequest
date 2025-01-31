using Entities;

namespace Services
{
    public interface IRatingService
    {
        Task<Rating> post(Rating rating);
    }
}