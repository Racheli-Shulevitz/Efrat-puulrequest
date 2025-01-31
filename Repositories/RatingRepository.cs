using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        OurStoreContext _OurStoreContext;
        public RatingRepository(OurStoreContext ourStoreContext)
        {
            _OurStoreContext = ourStoreContext;
        }
        public async Task<Rating> post(Rating rating)
        {
            await _OurStoreContext.AddAsync(rating);
            await _OurStoreContext.SaveChangesAsync();
            return rating;
        }
    }
}
