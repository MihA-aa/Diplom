using Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class TaskStatisticRepository : GenericRepository<TaskStatistic>
    {
        public TaskStatisticRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}