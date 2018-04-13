using Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class TopicRepository : GenericRepository<Topic>
    {
        public TopicRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}