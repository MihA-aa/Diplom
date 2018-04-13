using Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class RatingRepository : GenericRepository<Rating>
    {
        public RatingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}