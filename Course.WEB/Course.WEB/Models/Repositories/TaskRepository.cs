using Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class TaskRepository : GenericRepository<Task>
    {
        public TaskRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}