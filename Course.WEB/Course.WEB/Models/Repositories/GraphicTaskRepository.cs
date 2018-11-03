using Course.WEB.Models.Entities.Graphics;

namespace Course.WEB.Models.Repositories
{
    public class GraphicTaskRepository : GenericRepository<GraphicTask>
    {
        public GraphicTaskRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}