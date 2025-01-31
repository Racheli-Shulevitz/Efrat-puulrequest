using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        IRatingRepository ratingRepository;

        public RatingService(IRatingRepository _ratingRepository)
        {
            ratingRepository = _ratingRepository;
        }
        public async Task<Rating> post(Rating rating)
        {
            return await ratingRepository.post(rating);
        }
    }
}
