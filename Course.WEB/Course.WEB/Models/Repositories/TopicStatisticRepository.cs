using Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class TopicStatisticRepository : GenericRepository<TopicStatistic>
    {
        public TopicStatisticRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}